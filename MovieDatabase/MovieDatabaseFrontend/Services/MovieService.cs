using MovieDatabaseBackend.Dtos;
using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public class MovieService(HttpClient httpClient) : IMovieService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<IEnumerable<Movie>> GetMoviesAsync()
        {
            IEnumerable<MovieDto>? movies = null;
            try
            {
                var response = await _httpClient.GetAsync("http://localhost:5172/movies");
                if (response.IsSuccessStatusCode)
                {
                    movies = await response.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>();
                }
            }
            catch { }
            return movies?.Select(x => new Movie { Title = x.Title }) ?? [];
        }
    }
}
