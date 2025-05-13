namespace CommonFilms.DTOs;

public class GetMoviesParams
{
    public int Page { get; set; }
    public string Language { get; set; }
    public string SortBy { get; set; }
}