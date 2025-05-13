using CommonFilms.DTOs;
using CommonFilms.Models.Entities;

namespace CommonFilms.Services.MovieService;

public interface IMovieService
{
    public Task<MoviesList?> GetAllAsync(GetMoviesParams getMoviesParams);
    public Task<Movie?> GetByIdAsync(int id);
    public Task<Movie> CreateAsync(Movie movie);
    public Task<Movie> UpdateAsync(Movie movie);
    public Task<bool> DeleteAsync(int id);
    public Task<IEnumerable<Movie>> GetByUserIdAsync(int userId);
}