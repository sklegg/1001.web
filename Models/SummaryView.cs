using System.Text.Json.Serialization;

namespace Web1001.Models;

public class SummaryView
{
    [JsonPropertyName("reviewCount")]
    public int ReviewCount { get; set; }
    
    [JsonPropertyName("reviewAverage")]
    public double ReviewAverage { get; set;}

    [JsonPropertyName("bangers")]
    public List<SongSummaryView> Bangers { get; set; } = new ();

    [JsonPropertyName("duds")]
    public List<SongSummaryView> Duds { get; set; } = new ();
}

public class SongSummaryView
{
    [JsonPropertyName("artist")]
    public string? Artist { get; init; }
    
    [JsonPropertyName("title")]
    public string? Title { get; init; }
    
    [JsonPropertyName("year")]
    public int Year { get; init; }    
}