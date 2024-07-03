//using System.Net.Http.Headers;

namespace FilmsRanking.Helpers
{
    public class TMDBApiCaller
    {
        public static async Task<string> GetMoviesAsync()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.themoviedb.org/3/discover/movie"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJmYzE3OGRlMjQzYjQ1ZDY1ZmE3NTg0ODk2ZTM0N2YzOCIsIm5iZiI6MTcxOTk5NzQ0MC40MDc3MjIsInN1YiI6IjY2Njk3MDUxMmEzZmZmNTljZDdhYjYzYyIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.FPkWkGRpizushvQKRSUoF_fvwEkmtjTYsMrqmwC9xDw" },
                },
            };
            
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return body;
            }
            
        }
    }
}
