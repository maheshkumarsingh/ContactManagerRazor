//using ServiceContracts.DTOs;
//using ServiceContracts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Strategies.PersonSearchStrategy
//{
//    public class PersonSearchByCountryNameStrategy : IPersonSearchStrategy
//    {
//        private readonly IPersonService _personService;
//        private readonly ICountriesService _countriesService;

//        //public PersonSearchByCountryNameStrategy()
//        //{
//        //    _personService = new PersonsService();
//        //    _countriesService = new CountriesService();
//        //}
//        public List<PersonResponseDTO> GetFilteredPersons(string searchString)
//        {
//            List<PersonResponseDTO> allPersons = _personService.GetAllPersons();
//            if (string.IsNullOrEmpty(searchString))
//                return allPersons;

//            List<PersonResponseDTO> matchingPersons = new List<PersonResponseDTO>();
//            foreach (var item in allPersons)
//            {
//                CountryResponseDTO response = _countriesService.GetCountryByCountryID(item.CountryID);
//                item.CountryName = response.CountryName;
//                if (item.CountryName != null && item.CountryName.Contains(searchString))
//                    matchingPersons.Add(item);
//            }
//            return matchingPersons;           
//        }
//    }
//}
