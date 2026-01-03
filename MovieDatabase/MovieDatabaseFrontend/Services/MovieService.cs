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
                else if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    errorService.LogHttpResponse(response);
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

        public async Task<MovieDetailViewModel> CreateMovieAsync(MovieDetailViewModel movie)
        {
            var movieDto = new MovieCreateUpdateDto
            {
                Title = movie.Title,
                Plot = movie.Plot,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                AgeRating = movie.AgeRating,
                Genres = movie.Genres.Select(g => g.Id),
                Directors = movie.Directors.Select(p => p.Id),
                Writer = movie.Writer?.Id,
                Actors = movie.Actors.Select(p => p.Id),
                Duration = movie.Duration
            };
            MovieDetailDto? createdMovieDetailDto = null;
            try
            {
                var response = await httpClient.PostAsJsonAsync("http://localhost:5172/movies/", movieDto);
                if (response.IsSuccessStatusCode)
                {
                    createdMovieDetailDto = await response.Content.ReadFromJsonAsync<MovieDetailDto>();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    errorService.LogHttpResponse(response, "Der Film enthält fehlerhafte Daten und konnte deshalb nicht gespeichert werden.");
                }
                else
                {
                    errorService.LogHttpResponse(response);
                }
            }
            catch (Exception e)
            {
                errorService.LogError(e);
            }
        }

        public async Task<bool> UpdateMovieAsync(MovieDetailViewModel movie)
        {
            var movieDto = new MovieCreateUpdateDto
            {
                Title = movie.Title,
                Plot = movie.Plot,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                AgeRating = movie.AgeRating,
                Genres = movie.Genres.Select(g => g.Id),
                Directors = movie.Directors.Select(p => p.Id),
                Writer = movie.Writer?.Id,
                Actors = movie.Actors.Select(p => p.Id),
                Duration = movie.Duration
            };
            try
            {
                var response = await httpClient.PutAsJsonAsync("http://localhost:5172/movies/" + movie.Id, movieDto);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorService.LogMessage("Der Film konnte nicht bearbeitet werden, da er in der Datenbank nicht existiert.");
                }
                else if (!response.IsSuccessStatusCode)
                {
                    errorService.LogHttpResponse(response);
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                errorService.LogError(e);
            }
            return false;
        }

        public async Task<bool> DeleteMovieAsync(MovieViewModel movie)
        {
            try
            {
                var response = await httpClient.DeleteAsync("http://localhost:5172/movies/" + movie.Id);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorService.LogMessage("Der Film konnte nicht gelöscht werden, da er in der Datenbank nicht existiert.");
                }
                else if (!response.IsSuccessStatusCode)
                {
                    errorService.LogHttpResponse(response);
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                errorService.LogError(e);
            }
            return false;
        }
    }
}
