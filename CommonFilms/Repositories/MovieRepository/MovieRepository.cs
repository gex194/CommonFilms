using CommonFilms.Data;
using CommonFilms.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonFilms.Repositories.MovieRepository;

public class MovieRepository : IMovieRepository
{
    private readonly ApplicationDbContext _dbContext;

    public MovieRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _dbContext.Movies.ToListAsync();
    }
    
    public async Task<Movie?> GetByIdAsync(int id)
    {
        return await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task<Movie> CreateAsync(Movie movie)
    {
        await _dbContext.Movies.AddAsync(movie);
        await _dbContext.SaveChangesAsync();
        return movie;
    }

    public async Task<Movie> UpdateAsync(Movie movie)
    {
        _dbContext.Movies.Update(movie);
        await _dbContext.SaveChangesAsync();
        return movie;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var movie = await _dbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);
        if (movie == null)
        {
            return false;
        }
        
        _dbContext.Movies.Remove(movie);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}