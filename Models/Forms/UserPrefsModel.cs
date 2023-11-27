using System.Text.Json.Serialization;

namespace Web1001.Models.Forms;

public class UserPrefsModel {
    [JsonPropertyName("reviewSubmitAction")]
    public string? ReviewSubmitAction { get; set; }
    public static UserPrefsModel Defaults()
    {
        return new UserPrefsModel {
            ReviewSubmitAction = AfterSubmit.SongPage
        };
    }
}

static class AfterSubmit {
    public const string SongPage = "SONG";
    public const string HistoryPage = "HISTORY";
    public const string ReviewPage = "REVIEW";
}