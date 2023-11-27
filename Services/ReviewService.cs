using System.Text;
using System.Text.Json;
using Web1001.Shared.Config;
using Web1001.Models;

namespace Web1001.Services
{
    public interface IReviewService
    {
        Task<ReviewModel?> SubmitReview(ReviewModel review);
        Task<List<ReviewModel>> GetReviews(Guid songId);
    }

    public class ReviewService : IReviewService
    {
        private HttpClient httpClient;

        public ReviewService (HttpClient c, ICustomConfiguration config) {
            httpClient = c;
            if (httpClient.BaseAddress == null)
                httpClient.BaseAddress = new Uri(config.ApiBaseUrl);
        }

        public async Task<ReviewModel?> SubmitReview(ReviewModel review)
        {
            var content = new StringContent(JsonSerializer.Serialize<ReviewModel>(review), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/review/submit", content);
            var responseContent = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            if (responseContent != null)
            {
                ReviewModel? newReview = await JsonSerializer.DeserializeAsync<ReviewModel>(responseContent).ConfigureAwait(false);
                if (newReview != null)
                    return newReview;
            }

            return null;  
        }

        public async Task<List<ReviewModel>> GetReviews(Guid songId)
        {
            var response = await httpClient.GetAsync("/review?songId=" + songId);
            var responseContent = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            if (responseContent != null)
            {
                List<ReviewModel>? reviews = await JsonSerializer.DeserializeAsync<List<ReviewModel>>(responseContent).ConfigureAwait(false);
                if (reviews != null)
                    return reviews;
            }

            return new List<ReviewModel>();
        }
    }
}