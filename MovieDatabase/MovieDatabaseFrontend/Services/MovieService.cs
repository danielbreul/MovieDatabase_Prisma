using MovieDatabaseBackend.Dtos;
using MovieDatabaseFrontend.ViewModels;
using System.Net;

namespace MovieDatabaseFrontend.Services
{
    public class MovieService(HttpClient httpClient, IErrorService errorService) : IMovieService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IErrorService errorService = errorService;

        public async Task<IEnumerable<MovieViewModel>> GetMoviesAsync(string? title = null)
        {
            IEnumerable<MovieDto>? moviesDto = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(title))
                {
                    title = "/search?title=" + WebUtility.UrlEncode(title);
                }
                var response = await httpClient.GetAsync("http://localhost:5172/movies" + title);
                if (response.IsSuccessStatusCode)
                {
                    moviesDto = await response.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>();
                }
                else if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    errorService.LogHttpResponse(response);
                }
            }
            catch (HttpRequestException e)
            {
                errorService.LogMessage("Die Verbindung zum Backend ist unterbrochen: " + e.Message);
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
            catch (HttpRequestException e)
            {
                errorService.LogMessage("Die Verbindung zum Backend ist unterbrochen: " + e.Message);
            }
            catch (Exception e)
            {
                errorService.LogError(e);
            }

            if (movieDetailDto is not null)
            {
                return MovieDtoToViewModel(movieDetailDto);
            }
            else
            {
                return null;
            }
        }

        public async Task<MovieViewModel?> CreateMovieAsync(MovieDetailViewModel movie)
        {
            MovieDetailDto? createdMovieDetailDto = null;
            try
            {
                var response = await httpClient.PostAsJsonAsync("http://localhost:5172/movies/", MovieViewModelToDto(movie));
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
            catch (HttpRequestException e)
            {
                errorService.LogMessage("Die Verbindung zum Backend ist unterbrochen: " + e.Message);
            }
            catch (Exception e)
            {
                errorService.LogError(e);
            }

            if (createdMovieDetailDto is not null)
            {
                return new MovieViewModel { Id = createdMovieDetailDto.Id, Title = createdMovieDetailDto.Title };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateMovieAsync(MovieDetailViewModel movie)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("http://localhost:5172/movies/" + movie.Id, MovieViewModelToDto(movie));
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorService.LogMessage("Der Film konnte nicht bearbeitet werden, da er in der Datenbank nicht existiert.");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    errorService.LogHttpResponse(response, "Der Film enthält fehlerhafte Daten und konnte deshalb nicht gespeichert werden.");
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
            catch (HttpRequestException e)
            {
                errorService.LogMessage("Die Verbindung zum Backend ist unterbrochen: " + e.Message);
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
            catch (HttpRequestException e)
            {
                errorService.LogMessage("Die Verbindung zum Backend ist unterbrochen: " + e.Message);
            }
            catch (Exception e)
            {
                errorService.LogError(e);
            }
            return false;
        }

        private static MovieCreateUpdateDto MovieViewModelToDto(MovieDetailViewModel movie)
        {
            return new MovieCreateUpdateDto
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
        }

        private static MovieDetailViewModel MovieDtoToViewModel(MovieDetailDto movie)
        {
            return new MovieDetailViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Plot = movie.Plot,
                ReleaseDate = movie.ReleaseDate,
                Rating = movie.Rating,
                AgeRating = movie.AgeRating,
                Genres = movie.Genres.Select(g => new GenreViewModel { Id = g.Id, Name = g.Name }).ToList(),
                Directors = movie.Directors.Select(p => new PersonViewModel { Id = p.Id, Name = p.Name }).ToList(),
                Writer = movie.Writer is not null
                       ? new PersonViewModel { Id = movie.Writer.Id, Name = movie.Writer.Name }
                       : null,
                Actors = movie.Actors.Select(p => new PersonViewModel { Id = p.Id, Name = p.Name }).ToList(),
                Duration = movie.Duration,
            };
        }

    }
}