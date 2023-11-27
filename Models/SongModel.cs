using System.Text.Json.Serialization;

namespace Web1001.Models;

public class SongModel {
    [JsonPropertyName("id")]
    public string? SongId { get; set; }
    
    [JsonPropertyName("artist")]
    public string? ArtistName { get; set; }
    
    [JsonPropertyName("title")]
    public string? SongName { get; set; } 

    [JsonPropertyName("year")]
    public int? Year { get; set; }

    [JsonPropertyName("imageUri")]
    public string? ImageUri { get; set; }

    [JsonPropertyName("spotifyUri")]
    public string? SpotifyUri { get; set; }
}