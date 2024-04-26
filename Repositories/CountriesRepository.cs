using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly PersonsDbContext _dbContext;

        public CountriesRepository(PersonsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Country> AddCountry(Country country)
        {
            await _dbContext.Countries.AddAsync(country);
            await _dbContext.SaveChangesAsync();
            return country;
        }

        public async Task<List<Country>> GetAllCountries()
        {
            return await _dbContext.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryById(Guid? id)
        {
            return await _dbContext.Countries.FirstOrDefaultAsync(country => country.CountryID == id);
        }

        public async Task<Country?> GetCountryByName(string countryName)
        {
            return await _dbContext.Countries.FirstOrDefaultAsync(country => country.CountryName == countryName);
        }
    }
}
