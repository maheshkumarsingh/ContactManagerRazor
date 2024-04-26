//using Entities;
//using ServiceContracts;
//using ServiceContracts.DTOs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Strategies.PersonSearchStrategy
//{
//    internal class PersonSearchByNameStrategy : IPersonSearchStrategy
//    {
//        private readonly IPersonService _personService;

//        public PersonSearchByNameStrategy()
//        {
            
//        }
//        public PersonSearchByNameStrategy(IPersonService personService)
//        {
//            _personService = personService;
//        }

//        public List<PersonResponseDTO> GetFilteredPersons(string searchString)
//        {
//            List<PersonResponseDTO> allPeoples = _personService.GetAllPersons();
//            List<PersonResponseDTO> filteredPeoples = null;
//            filteredPeoples = allPeoples.Where(
//                                               temp=> (!string.IsNullOrEmpty(temp.PersonName) ? 
//                                               temp.PersonName.Contains(searchString,StringComparison.OrdinalIgnoreCase) :
//                                               true))
//                                               .ToList();
//            return filteredPeoples;
//        }
//    }
//}
