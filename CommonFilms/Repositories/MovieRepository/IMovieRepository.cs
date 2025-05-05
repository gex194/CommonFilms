using CommonFilms.Models.Entities;

namespace CommonFilms.Repositories.MovieRepository;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllAsync();
    Task<Movie?> GetByIdAsync(int id);
    Task<Movie> CreateAsync(Movie movie);
    Task<Movie> UpdateAsync(Movie movie);
    Task<bool> DeleteAsync(int id);
}