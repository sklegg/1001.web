using Web1001.Models.Forms;

namespace Web1001.Shared;

public class GlobalState
{
    /* https://learn.microsoft.com/en-us/aspnet/core/blazor/state-management?view=aspnetcore-7.0&pivots=webassembly#in-memory-state-container-service-wasm */
    private UserPrefsModel? userPrefs;
    private bool loggedInFlag;
    private string? userId;


    public bool LoggedIn 
    {
        get => loggedInFlag;
        set
        {
            loggedInFlag = value;
            NotifyStateChanged();
        }    
    }

    public string? UserId 
    {
        get => userId;
        set
        {
            userId = value;
            NotifyStateChanged();
        }    
    }    

    public UserPrefsModel UserPreferences 
    { 
        get => userPrefs ?? UserPrefsModel.Defaults(); 
        set
        {
            userPrefs = value;
            NotifyStateChanged();
        } 
    }

    public event Action? OnChange;

    private void NotifyStateChanged()
    {
        OnChange?.Invoke();
    }     
}