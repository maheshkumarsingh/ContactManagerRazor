using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTOs;
using System.Runtime.CompilerServices;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //private readonly List<Country> _countries = null;

        private readonly ICountriesRepository _countriesRepository;
        public CountriesService(ICountriesRepository countriesRepository)
        {
            _countriesRepository = countriesRepository;
            #region old seed data
            //_countries = new List<Country>();
            //if (initialiaze)
            //{
            //    _countries.AddRange(new List<Country>()
            //    {
            //        new Country() { CountryID = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D"), CountryName = "India" },
            //        new Country() { CountryID = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B"), CountryName = "USA" },
            //        new Country() { CountryID = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F"), CountryName = "Canada" },
            //        new Country() { CountryID = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E"), CountryName = "UK" },
            //        new Country() { CountryID = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB"), CountryName = "Australia" }
            //    });
            //}//{3BA967EC-DD57-4ABD-9B05-21E8DABA8B84}
            #endregion
        }
        public async Task<CountryResponseDTO> AddCountry(AddCountryRequestDTO? countryRequestDTO)
        {
            if(countryRequestDTO?.CountryName == null)
            {
                throw new ArgumentNullException(nameof(countryRequestDTO.CountryName));
            }
            else if(await _countriesRepository.GetCountryByName(countryRequestDTO.CountryName) != null)
            {
                throw new ArgumentNullException("Duplicate Country Cannot be Added");
            }
            Country country = countryRequestDTO.ToCountry();
            country.CountryID = Guid.NewGuid();
            await _countriesRepository.AddCountry(country);
            return country.ToCountryResponseDTO();
        }

        public async Task<List<CountryResponseDTO>> GetAllCountries()
        {
            List<Country> countries = await _countriesRepository.GetAllCountries();
            return countries.Select(temp => temp.ToCountryResponseDTO()).ToList();
        }
        /// <summary>
        /// Return CountryResponse by country ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<CountryResponseDTO> GetCountryByCountryID(Guid? id)
        {
            if (id == null) throw new ArgumentNullException("id");

            Country country = await _countriesRepository.GetCountryById(id);
            if (country == null)
                return null;
            return country.ToCountryResponseDTO();
        }

        public async Task<int> UploadCountriesFromExcelFile(IFormFile formFile)
        {
            MemoryStream memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream);

            int countriesInsertCount = 0;
            using(ExcelPackage  package = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Countries"];
                int rowCount = worksheet.Dimension.Rows;
                for(int row=2; row<=rowCount; row++)
                {
                    string? cellValue = worksheet.Cells[row, 1].Value.ToString();
                    if(!string.IsNullOrEmpty(cellValue))
                    {
                        string? countryName = cellValue;
                        if(await _countriesRepository.GetCountryByName(countryName) == null)
                        {
                            Country country = new Country()
                            {
                                CountryName = countryName
                            };

                            await _countriesRepository.AddCountry(country);
                            countriesInsertCount++;
                        }
                    }
                }
            }
            return countriesInsertCount;
        }
    }
}
