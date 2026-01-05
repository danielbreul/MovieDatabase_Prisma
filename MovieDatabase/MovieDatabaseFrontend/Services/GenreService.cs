using MovieDatabaseBackend.Dtos;
using MovieDatabaseFrontend.ViewModels;
using System.Net;

namespace MovieDatabaseFrontend.Services
{
    public class GenreService(HttpClient httpClient, IErrorService errorService) : IGenreService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IErrorService errorService = errorService;

        public async Task<IEnumerable<GenreViewModel>> GetGenresAsync()
        {
            IEnumerable<GenreDto>? genresDto = null;
            try
            {
                var response = await httpClient.GetAsync("genres");
                if (response.IsSuccessStatusCode)
                {
                    genresDto = await response.Content.ReadFromJsonAsync<IEnumerable<GenreDto>>();
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

            return genresDto?.Select(g => new GenreViewModel { Id = g.Id, Name = g.Name }) ?? [];
        }

        public async Task<GenreViewModel?> CreateGenreAsync(GenreViewModel genre)
        {
            GenreDto? createdGenreDto = null;
            try
            {
                var response = await httpClient.PostAsJsonAsync("genres/", new GenreCreateUpdateDto { Name = genre.Name });
                if (response.IsSuccessStatusCode)
                {
                    createdGenreDto = await response.Content.ReadFromJsonAsync<GenreDto>();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    errorService.LogHttpResponse(response, "Das Genre enthält fehlerhafte Daten und konnte deshalb nicht gespeichert werden.");
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

            if (createdGenreDto is not null)
            {
                return new GenreViewModel { Id = createdGenreDto.Id, Name = createdGenreDto.Name };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdateGenreAsync(GenreViewModel genre)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("genres/" + genre.Id, new GenreCreateUpdateDto { Name = genre.Name });
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorService.LogMessage("Das Genre konnte nicht bearbeitet werden, da es in der Datenbank nicht existiert.");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    errorService.LogHttpResponse(response, "Das Genre enthält fehlerhafte Daten und konnte deshalb nicht gespeichert werden.");
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

        public async Task<bool> DeleteGenreAsync(GenreViewModel genre)
        {
            try
            {
                var response = await httpClient.DeleteAsync("genres/" + genre.Id);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorService.LogMessage("Das Genre konnte nicht gelöscht werden, da es in der Datenbank nicht existiert.");
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    errorService.LogMessage("Das Genre konnte nicht gelöscht werden, da es in mindestens einem Film referenziert ist.");
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

        public async Task<IEnumerable<MovieViewModel>> GetMoviesAsync(int id)
        {
            IEnumerable<MovieDto>? moviesDto = null;
            try
            {
                var response = await httpClient.GetAsync($"genres/{id}/movies");
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
    }
}
