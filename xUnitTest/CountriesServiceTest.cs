using ServiceContracts;
using ServiceContracts.DTOs;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTest
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;
        
        //public CountriesServiceTest()
        //{
        //    _countriesService = new CountriesService();
        //}
        #region Add countries
        //[Fact]
        //public async Task AddCountry_NullCountry()
        //{
        //    AddCountryRequestDTO requestDTO = null;

        //    await Assert.ThrowsAsync<ArgumentNullException>(() =>
        //    {
        //        _countriesService.AddCountry(requestDTO);
        //    });
        //}


        //[Fact]
        //public async Task AddCountry_CountryNameNull()
        //{
        //    AddCountryRequestDTO requestDTO = new AddCountryRequestDTO() { CountryName = null};

        //    await Assert.ThrowsAsync<ArgumentException>(() =>
        //    {
        //        _countriesService.AddCountry(requestDTO);
        //    });
        //}
        
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            AddCountryRequestDTO requestDTO = new AddCountryRequestDTO() { CountryName = "USA"};
            AddCountryRequestDTO requestDTO1 = new AddCountryRequestDTO() { CountryName = "USA"};

            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(requestDTO);
                _countriesService.AddCountry(requestDTO1);
            });
        }

        [Fact]
        public async Task AddCountry_ProperCountryNameAsync()
        {
            AddCountryRequestDTO requestDTO = new AddCountryRequestDTO() { CountryName = "Japan"};

            CountryResponseDTO addCountryResponseDTO = await _countriesService.AddCountry(requestDTO);
            
            Assert.True(addCountryResponseDTO.CountryID != Guid.Empty);
        }
        #endregion

        #region Gell All countries
        [Fact]
        public async Task GetAllCountries_EmptyListAsync()
        {
            List<CountryResponseDTO> countryResponseDTOs = await _countriesService.GetAllCountries();
            Assert.Empty(countryResponseDTOs);
        }

        [Fact]

        public void GetAllCountries_AddFewCountries()
        {
            List<AddCountryRequestDTO> countryRequestDTOs = new List<AddCountryRequestDTO>()
            {
                new AddCountryRequestDTO{CountryName = "USA"},
                new AddCountryRequestDTO{CountryName = "UK"}
            };

        }
        #endregion
    }
}
