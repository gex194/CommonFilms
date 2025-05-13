using CommonFilms.Enums;

namespace CommonFilms.Models.Entities;

public class Movie
{
    public int Id { get; set; }
    public List<Genre> Genres { get; set; }
    public string? Title { get; set; }
    public string? PosterPath { get; set; }
    public string? BackdropPath { get; set; }
    public string? Overview { get; set; }
    public string? ReleaseDate { get; set; }
    public string? OriginalTitle { get; set; }
    public string? OriginalLanguage { get; set; }
    public int? VoteCount { get; set; }
    public double? VoteAverage { get; set; }
    public bool? Video { get; set; }
    public int UserId { get; set; }
}