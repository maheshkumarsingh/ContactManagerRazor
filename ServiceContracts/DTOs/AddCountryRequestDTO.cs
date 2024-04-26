using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs
{
    public class AddCountryRequestDTO
    {
        public string? CountryName
        {
            get
            {
                return CountryName;
            }
            set
            {
                CountryName = value;
            }
        }
        public Country ToCountry()
        {
            return new Country() { CountryName = this.CountryName };
        }
    }

}
