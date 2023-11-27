using System.Text.Json.Serialization;

namespace Web1001.Models;

public class HistoryReviewModel
{
    [JsonPropertyName("review")]
    public ReviewModel Review { get; set; }
    
    [JsonPropertyName("reviewedSong")]
    public SongModel ReviewedSong { get; set;}

    [JsonPropertyName("count")]
    public int ReviewCount { get; set; }

    [JsonPropertyName("average")]
    public double ReviewAverage { get; set; }
}