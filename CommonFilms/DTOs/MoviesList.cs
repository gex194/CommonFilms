using CommonFilms.Models.Entities;

namespace CommonFilms.DTOs;

public class MoviesList
{
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public int TotalResults { get; set; }
    public List<Movie> Results { get; set; }
}