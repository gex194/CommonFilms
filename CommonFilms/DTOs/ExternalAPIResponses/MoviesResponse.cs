using System.Text.Json.Serialization;

namespace CommonFilms.DTOs;

public class MoviesResponse
{
    [JsonPropertyName("page")]
    public int Page { get; set; }
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
    [JsonPropertyName("total_results")]
    public int TotalResults { get; set; }
    [JsonPropertyName("results")]
    public List<MovieListResponse> Results { get; set; }
}