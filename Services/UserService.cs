namespace Web1001.Services;

using System.Text;
using System.Text.Json;
using Web1001.Models.Forms;
using Web1001.Models;
using Web1001.Shared.Config;
using Web1001.Shared;

public interface IUserService {
    Task<bool> Login(LoginFormModel form);
    Task Logout();
    Task<UserPrefsModel> GetPreferences();
    Task UpdatePreferences(UserPrefsModel form);
    Task<bool> CreateUserAccount(LoginFormModel form);
    Task<Guid?> GetUsersCurrentSongId();
    Task<bool> CheckCredentials();
    Task<List<HistoryReviewModel>> GetReviews();
    Task<SummaryView?> GetSummary(string linkValue);
    Task<string> GetShareLink();
}

public class UserService : IUserService
{
    private HttpClient httpClient;
    private GlobalState globalState;
    
    public UserService (HttpClient c, ICustomConfiguration config, GlobalState state) 
    {
        globalState = state;
        httpClient = c;
        if (httpClient.BaseAddress == null)
            httpClient.BaseAddress = new Uri(config.ApiBaseUrl);
    }

    public async Task<bool> CheckCredentials()
    {
        try {
            var response = await httpClient.GetAsync("/user/me");
            if (response.IsSuccessStatusCode)
            {
                string userString = await response.Content.ReadAsStringAsync();
                UserModel? user = JsonSerializer.Deserialize<UserModel>(userString);
                if (user != null)
                    globalState.UserId = user.UserId;
            } else {
                globalState.UserId = null;
            }

            return response.IsSuccessStatusCode;
        } catch (Exception) {

        }
        return false;
    }

    public async Task<bool> CreateUserAccount(LoginFormModel form)
    {
        var content = new StringContent(JsonSerializer.Serialize(form), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/user/create", content);
        return response.IsSuccessStatusCode;  
    }

    public async Task<bool> Login(LoginFormModel form) 
    {
        var content = new StringContent(JsonSerializer.Serialize(form), Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("/user/login", content);
        return response.IsSuccessStatusCode;
    }

    public async Task Logout()
    {
        try {
            _ = await httpClient.PostAsync("/user/logout", null);
        } catch (Exception) {

        }
    }

    public async Task<UserPrefsModel> GetPreferences()
    {
        string content = await httpClient.GetStringAsync("/user/prefs");
        UserPrefsModel? prefs = JsonSerializer.Deserialize<UserPrefsModel>(content);
        if (prefs == null)
        {
            globalState.UserPreferences = new UserPrefsModel();
        } else {
            globalState.UserPreferences = prefs;
        }

        return globalState.UserPreferences;
    }

    public async Task UpdatePreferences(UserPrefsModel form)
    {
        var content = new StringContent(JsonSerializer.Serialize(form), Encoding.UTF8, "application/json");
        _ = await httpClient.PutAsync("/user/prefs", content);
        globalState.UserPreferences = form;
    }

    public async Task<Guid?> GetUsersCurrentSongId()
    {
        string content = await httpClient.GetStringAsync("/user/me");
        UserModel? user = JsonSerializer.Deserialize<UserModel>(content);
        if (user != null && user.CurrentSongId != null)
        {
            return user.CurrentSongId;
        } else { 
            return null;
        }
    }

    public async Task<List<HistoryReviewModel>> GetReviews()
    {
        string content = await httpClient.GetStringAsync("/user/me/reviews");
        var reviewResponse = JsonSerializer.Deserialize<List<HistoryReviewModel>>(content);
        if (reviewResponse != null)
        {
            return reviewResponse;
        } else { 
            return new List<HistoryReviewModel>();
        }        
    }

    public async Task<SummaryView?> GetSummary(string linkValue)
    {
        if (string.IsNullOrEmpty(linkValue)) return null;
        string content = await httpClient.GetStringAsync("/user/summary/" + linkValue);
        var summary = JsonSerializer.Deserialize<SummaryView>(content);
        if (summary != null)
        {
            return summary;
        } else {
            return null;
        }
    }

    public async Task<string> GetShareLink()
    {
        var response = await httpClient.PostAsync("/user/sharelink", null);
        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(content))
            {
                return content;
            }
        }

        return string.Empty;
    }
}