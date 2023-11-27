using System.Text.Json.Serialization;

namespace Web1001.Models;

public class UserModel {
    [JsonPropertyName("id")]
    public string? UserId { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("reviews")]
    public List<ReviewModel>? Reviews { get; set; }

    [JsonPropertyName("currentSongId")]
    public Guid? CurrentSongId { get; set; }

    [JsonPropertyName("preferences")]
    public UserPrefs? Preferences { get; set; }
}

public class UserPrefs {
    [JsonPropertyName("reviewSubmitAction")]
    public string? ReviewSubmitAction { get; set; }
}