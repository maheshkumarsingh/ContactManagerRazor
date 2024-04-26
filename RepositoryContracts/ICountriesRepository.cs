using Entities;

namespace RepositoryContracts
{
    public interface ICountriesRepository
    {
        public Task<Country> AddCountry(Country country);

        public Task<List<Country>> GetAllCountries();

        public Task<Country?> GetCountryById(Guid? id);

        public Task<Country?> GetCountryByName(string countryName);

    }
}
