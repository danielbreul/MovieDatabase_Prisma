using MovieDatabaseBackend.Dtos;
using MovieDatabaseFrontend.ViewModels;
using System.Net;

namespace MovieDatabaseFrontend.Services
{
    public class MovieService(HttpClient httpClient, IErrorService errorService) : IMovieService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IErrorService errorService = errorService;

        public async Task<IEnumerable<MovieViewModel>> GetMoviesAsync()
        {
            IEnumerable<MovieDto>? moviesDto = null;
            try
            {
                var response = await httpClient.GetAsync("http://localhost:5172/movies");
                if (response.IsSuccessStatusCode)
                {
                    moviesDto = await response.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>();
                }
                else if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    errorService.LogHttpResponse(response);
                }
            }
            catch (Exception e)
            {
                errorService.LogError(e);
            }
            return moviesDto?.Select(x => new MovieViewModel { Id = x.Id, Title = x.Title }) ?? [];
        }

        public async Task<MovieDetailViewModel?> GetMovieDetailAsync(MovieViewModel movie)
        {
            MovieDetailDto? movieDetailDto = null;
            try
            {
                var response = await httpClient.GetAsync("http://localhost:5172/movies/" + movie.Id);
                if (response.IsSuccessStatusCode)
                {
                    movieDetailDto = await response.Content.ReadFromJsonAsync<MovieDetailDto>();
                }
            }
            catch (Exception e)
            {
                errorService.LogError(e);
            }
            if (movieDetailDto is not null)
            {
                return new MovieDetailViewModel
                {
                    Id = movieDetailDto.Id,
                    Title = movieDetailDto.Title,
                    Plot = movieDetailDto.Plot,
                    ReleaseDate = movieDetailDto.ReleaseDate,
                    Rating = movieDetailDto.Rating,
                    AgeRating = movieDetailDto.AgeRating,
                    Genres = movieDetailDto.Genres.Select(g => new GenreViewModel { Id = g.Id, Name = g.Name }),
                    Directors = movieDetailDto.Directors.Select(p => new PersonViewModel { Id = p.Id, Name = p.Name }),
                    Writer = movieDetailDto.Writer is not null
                        ? new PersonViewModel { Id = movieDetailDto.Writer.Id, Name = movieDetailDto.Writer.Name }
                        : null,
                    Actors = movieDetailDto.Actors.Select(p => new PersonViewModel { Id = p.Id, Name = p.Name }),
                    Duration = movieDetailDto.Duration,
                };
            }
            else
            {
                return null;
            }
        }
    }
}
