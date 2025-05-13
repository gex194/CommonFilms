using System.Text.Json;
using CommonFilms.DTOs;
using CommonFilms.Models.Entities;
using CommonFilms.Repositories.MovieRepository;

namespace CommonFilms.Services.MovieService;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public MovieService(IMovieRepository movieRepository, IHttpClientFactory httpClientFactory)
    {
        _movieRepository = movieRepository;
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<MoviesList?> GetAllAsync(GetMoviesParams getMoviesParams)
    {
        var client = _httpClientFactory.CreateClient("TheMovieDB");
        var query = BuildQuery(getMoviesParams);

        try
        {
            var response = await client.GetAsync("discover/movie" + query);
            response.EnsureSuccessStatusCode();

            var responseData = await response.Content.ReadAsStringAsync();
            var movieData = JsonSerializer.Deserialize<MoviesResponse>(responseData);
            
            var moviesList = new List<Movie>();
            if (movieData == null) return null;
            foreach (var item in movieData.Results)
            {
                var movie = new Movie
                {
                    Id = item.Id,
                    Genres = item.Genres,
                    Title = item.Title,
                    PosterPath = item.PosterPath,
                    BackdropPath = item.BackdropPath,
                    Overview = item.Overview,
                    ReleaseDate = item.ReleaseDate,
                    OriginalTitle = item.OriginalTitle,
                    OriginalLanguage = item.OriginalLanguage,
                    VoteCount = item.VoteCount,
                    VoteAverage = item.VoteAverage,
                    Video = item.Video
                };
                
                moviesList.Add(movie);
            }
            var result = new MoviesList()
            {
                Results = moviesList,
                Page = movieData.Page,
                TotalPages = movieData.TotalPages,
                TotalResults = movieData.TotalResults
            };

            return result;
        }
        catch (HttpRequestException ex)
        {
            return null;
        }
    }
    
    public async Task<Movie?> GetByIdAsync(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        return movie;
    }
    
    public async Task<Movie> CreateAsync(Movie movie)
    {
        return await _movieRepository.CreateAsync(movie);
    }
    
    public async Task<Movie> UpdateAsync(Movie movie)
    {
        return await _movieRepository.UpdateAsync(movie);
    }
    
    public async Task<bool> DeleteAsync(int id)
    {
        return await _movieRepository.DeleteAsync(id);
    }
    
    public async Task<IEnumerable<Movie>> GetByUserIdAsync(int userId)
    {
        var movies = await _movieRepository.GetAllAsync();
        return movies.Where(x => x.UserId == userId);
    }

    private string BuildQuery(GetMoviesParams getMoviesParams)
    {
        var propertiesInfo = typeof(GetMoviesParams).GetProperties();
        var queryStringList = propertiesInfo
            .Where(x => x.GetValue(getMoviesParams) != null)
            .Select(property =>property.Name.ToString().ToLower() + "=" + property.GetValue(getMoviesParams))
            .ToList();
        return "?" + queryStringList.Aggregate((a, b) => a + "&" + b);
    }
}