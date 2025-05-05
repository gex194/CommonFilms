using CommonFilms.Models.Entities;
using CommonFilms.Repositories.MovieRepository;

namespace CommonFilms.Services.MovieService;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    
    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        var movies = await _movieRepository.GetAllAsync();
        return movies;
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
}