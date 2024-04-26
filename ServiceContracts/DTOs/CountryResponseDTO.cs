using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs
{
    /// <summary>
    /// Response DTO for country class
    /// </summary>
    public class CountryResponseDTO
    {
        public Guid CountryID { get; set; }
        public string? CountryName { get; set; }

    }

    public static class CountryExtensions
    {
        public static CountryResponseDTO ToCountryResponseDTO(this Country country)
        {
            return new CountryResponseDTO() { CountryID = country.CountryID, CountryName = country.CountryName};
        }

        
    }
}
