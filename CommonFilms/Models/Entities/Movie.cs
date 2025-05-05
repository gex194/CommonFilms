using CommonFilms.Enums;

namespace CommonFilms.Models.Entities;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public int Year { get; set; }
    public required List<Genre> Genres { get; set; }
    public string? Image { get; set; }
    public float Rating { get; set; }
    public int UserId { get; set; }
}