//using ServiceContracts;
//using ServiceContracts.DTOs;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Services.Strategies.PersonSearchStrategy
//{
//    public class PersonSearchByAgeStrategy : IPersonSearchStrategy
//    {

//        private readonly IPersonService _personService;

//        //public PersonSearchByAgeStrategy()
//        //{
//        //    _personService = new PersonsService();
//        //}
//        public List<PersonResponseDTO> GetFilteredPersons(string searchString)
//        {
//            List<PersonResponseDTO> allPersons = _personService.GetAllPersons();
//            if (string.IsNullOrEmpty(searchString))
//                return allPersons;

//            List<PersonResponseDTO> matchingPerson = null;
//            matchingPerson = allPersons.Where(temp => (temp.DOB != null) ? temp.DOB.Value.ToString("dd MM yyyy").Contains(searchString, StringComparison.OrdinalIgnoreCase) : true).ToList();

//            return matchingPerson;
//        }
//    }
//}
