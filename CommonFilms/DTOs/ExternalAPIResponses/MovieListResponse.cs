using System.Text.Json.Serialization;
using CommonFilms.Enums;

namespace CommonFilms.DTOs;

public class MovieListResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("genre_ids")]
    public List<Genre> Genres { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; set; }
    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; set; }
    [JsonPropertyName("overview")]
    public string? Overview { get; set; }
    [JsonPropertyName("release_date")]
    public string? ReleaseDate { get; set; }
    [JsonPropertyName("original_title")]
    public string? OriginalTitle { get; set; }
    [JsonPropertyName("original_language")]
    public string? OriginalLanguage { get; set; }
    [JsonPropertyName("vote_count")]
    public int? VoteCount { get; set; }
    [JsonPropertyName("vote_average")]
    public double? VoteAverage { get; set; }
    [JsonPropertyName("video")]
    public bool? Video { get; set; }
}