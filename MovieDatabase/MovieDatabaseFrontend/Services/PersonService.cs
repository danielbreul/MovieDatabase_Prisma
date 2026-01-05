using MovieDatabaseBackend.Dtos;
using MovieDatabaseFrontend.ViewModels;
using System.Net;

namespace MovieDatabaseFrontend.Services
{
    public class PersonService(HttpClient httpClient, IErrorService errorService) : IPersonService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly IErrorService errorService = errorService;

        public async Task<IEnumerable<PersonViewModel>> GetPersonsAsync()
        {
            IEnumerable<PersonDto>? personsDto = null;
            try
            {
                var response = await httpClient.GetAsync("persons");
                if (response.IsSuccessStatusCode)
                {
                    personsDto = await response.Content.ReadFromJsonAsync<IEnumerable<PersonDto>>();
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

            return personsDto?.Select(g => new PersonViewModel { Id = g.Id, Name = g.Name }) ?? [];
        }

        public async Task<PersonViewModel?> CreatePersonAsync(PersonViewModel person)
        {
            PersonDto? createdPersonDto = null;
            try
            {
                var response = await httpClient.PostAsJsonAsync("persons/", new PersonCreateUpdateDto { Name = person.Name });
                if (response.IsSuccessStatusCode)
                {
                    createdPersonDto = await response.Content.ReadFromJsonAsync<PersonDto>();
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    errorService.LogHttpResponse(response, "Die Person enthält fehlerhafte Daten und konnte deshalb nicht gespeichert werden.");
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

            if (createdPersonDto is not null)
            {
                return new PersonViewModel { Id = createdPersonDto.Id, Name = createdPersonDto.Name };
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> UpdatePersonAsync(PersonViewModel person)
        {
            try
            {
                var response = await httpClient.PutAsJsonAsync("persons/" + person.Id, new PersonCreateUpdateDto { Name = person.Name });
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorService.LogMessage("Die Person konnte nicht bearbeitet werden, da sie in der Datenbank nicht existiert.");
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    errorService.LogHttpResponse(response, "Die Person enthält fehlerhafte Daten und konnte deshalb nicht gespeichert werden.");
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

        public async Task<bool> DeletePersonAsync(PersonViewModel person)
        {
            try
            {
                var response = await httpClient.DeleteAsync("persons/" + person.Id);
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    errorService.LogMessage("Die Person konnte nicht gelöscht werden, da sie in der Datenbank nicht existiert.");
                }
                else if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    errorService.LogMessage("Die Person konnte nicht gelöscht werden, da sie in mindestens einem Film referenziert ist.");
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

        public Task<IEnumerable<MovieViewModel>> GetDirectedMoviesAsync(int id)
        {
            return GetMoviesAsync(id, "directed-movies");
        }

        public Task<IEnumerable<MovieViewModel>> GetWrittenMoviesAsync(int id)
        {
            return GetMoviesAsync(id, "written-movies");
        }

        public Task<IEnumerable<MovieViewModel>> GetActedInMoviesAsync(int id)
        {
            return GetMoviesAsync(id, "acted-in-movies");
        }

        private async Task<IEnumerable<MovieViewModel>> GetMoviesAsync(int id, string url)
        {
            IEnumerable<MovieDto>? moviesDto = null;
            try
            {
                var response = await httpClient.GetAsync($"persons/{id}/{url}");
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
