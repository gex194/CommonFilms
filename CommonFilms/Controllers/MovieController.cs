using CommonFilms.DTOs;
using CommonFilms.Services.MovieService;
using Microsoft.AspNetCore.Mvc;

namespace CommonFilms.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovieList([FromQuery] GetMoviesParams getMoviesParams)
    {
        var movies = await _movieService.GetAllAsync(getMoviesParams);
        if (movies == null)
        {
            return NotFound();
        }
        return Ok(movies);
    }
}