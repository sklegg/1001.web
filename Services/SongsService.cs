using System.Text.Json;
using System.Text.Json.Serialization;
using Web1001.Shared.Config;
using Web1001.Models;

namespace Web1001.Services
{
    public interface ISongsService
    {
        Task<List<SongModel>> GetSongsAsync();
        Task<SongModel> GetSongById(string songId);
    }

    public class SongsService : ISongsService
    {
        private HttpClient httpClient;

        public SongsService (HttpClient c, ICustomConfiguration config) 
        {
            httpClient = c;
            if (httpClient.BaseAddress == null)
                httpClient.BaseAddress = new Uri(config.ApiBaseUrl);
        }

        public async Task<List<SongModel>> GetSongsAsync()
        {
            var result = await httpClient.GetAsync("song");
            if (result.IsSuccessStatusCode) {
                var content = await result.Content.ReadAsStreamAsync();
                if (content != null) 
                {
                    var jsonContent = await JsonSerializer.DeserializeAsync<Result<SongModel>>(content);
                    if (jsonContent != null && jsonContent.Results != null)
                    {
                        return jsonContent.Results;
                    }
                }
            }

            return new List<SongModel>();
        }

        public async Task<SongModel> GetSongById(string songId)
        {
            var result = await httpClient.GetAsync("song/" + songId);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStreamAsync();
                if (content != null)
                {
                    var jsonContent = await JsonSerializer.DeserializeAsync<SongModel>(content);
                    if (jsonContent != null)
                    {
                        return jsonContent;
                    }
                }
            }
            
            return new SongModel();
        }
    }

    public class Result<T>
    {
        [JsonPropertyName("result")]
        public List<T>? Results { get; set; }
    }
}