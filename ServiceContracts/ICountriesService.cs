using Microsoft.AspNetCore.Http;
using ServiceContracts.DTOs;

namespace ServiceContracts
{
    /// <summary>
    /// Represents business logic for country add
    /// </summary>
    public interface ICountriesService
    {
        public Task<CountryResponseDTO> AddCountry(AddCountryRequestDTO? addCountryRequestDTO);

        /// <summary>
        /// Return all countries
        /// </summary>
        /// <returns></returns>
        public Task<List<CountryResponseDTO>> GetAllCountries();

        public Task<CountryResponseDTO> GetCountryByCountryID(Guid? id);
        public Task<int> UploadCountriesFromExcelFile(IFormFile formFile);
    }
}
