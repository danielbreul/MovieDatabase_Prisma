using MovieDatabaseFrontend.ViewModels;

namespace MovieDatabaseFrontend.Services
{
    public interface IPersonService
    {
        public Task<IEnumerable<PersonViewModel>> GetPersonsAsync();
        public Task<PersonViewModel?> CreatePersonAsync(PersonViewModel genre);
        public Task<bool> UpdatePersonAsync(PersonViewModel genre);
        public Task<bool> DeletePersonAsync(PersonViewModel genre);
    }
}
