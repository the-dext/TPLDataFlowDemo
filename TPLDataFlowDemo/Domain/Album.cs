namespace Domain;

public class Album(string Title, string Genre, int ReleaseYear, string ArtistName)
{
    public string Title { get; set; } = Title;
    public string Genre { get; set; } = Genre;
    public int ReleaseYear { get; set; } = ReleaseYear;
    public string ArtistName { get; set; } = ArtistName;
}