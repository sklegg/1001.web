namespace Web1001.Shared.Config;

public interface ICustomConfiguration
{
    public string ApiBaseUrl { get; set; }
}

public class CustomConfiguration : ICustomConfiguration
{
    public CustomConfiguration()
    {        
        ApiBaseUrl = "http://localhost:5050";
    }

    public string ApiBaseUrl { get; set; }
}
