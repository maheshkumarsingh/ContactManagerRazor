using ServiceContracts.DTOs;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface IPersonService
    {
        Task<PersonResponseDTO> AddPerson(AddPersonRequestDTO requestDTO);
        Task<List<PersonResponseDTO>> GetAllPersons();
        Task<PersonResponseDTO> GetPersonByID(Guid? id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchBy">Search field to search</param>
        /// <param name="searchString">returns all matching persons.</param>
        /// <returns></returns>
        Task<List<PersonResponseDTO>> GetFilteredPersons(string searchBy, string searchString);

        /// <summary>
        /// This method is to sort person on demand
        /// </summary>
        /// <param name="sortBy"></param>
        /// <param name="sortOrderOptionType"></param>
        /// <returns>List of all persons in sorted manner</returns>
        Task<List<PersonResponseDTO>> GetSortedPersons(List<PersonResponseDTO> filteredPersons, string sortBy, SortOrderOptionType sortOrderOptionType);

        public Task<PersonResponseDTO> UpdatePerson(UpdatePersonRequestDTO requestDTO);

        public Task<bool> DeletePerson(Guid? personID);

        public Task<MemoryStream> GetPersonsCSV();

        public Task<MemoryStream> GetPersonsExcel();
    }
}
