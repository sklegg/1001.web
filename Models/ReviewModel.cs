using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Web1001.Models;

public class ReviewModel
{
    [JsonPropertyName("reviewId")]
    public Guid? ReviewId { get; set; }

    [JsonPropertyName("userId")]
    public Guid ReviewerId { get; set;}
    
    [JsonPropertyName("songId")]
    public Guid? SongId { get; set;}
    
    [Required]
    [Range(1, 5, ErrorMessage = "You must rate this song between 1 and 5 stars.")]
    [JsonPropertyName("stars")]
    public int StarRating { get; set;}
    
    [JsonPropertyName("comment")]
    public string Comment { get; set;}
    
    [JsonPropertyName("dateTime")]
    public DateTime PostedDateTime { get; set;}
}