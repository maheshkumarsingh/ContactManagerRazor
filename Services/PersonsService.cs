using CsvHelper;
using CsvHelper.Configuration;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using RepositoryContracts;
using Serilog;
using SerilogTimings;
using ServiceContracts;
using ServiceContracts.DTOs;
using ServiceContracts.Enums;
using Services.Helpers;
//using Services.Strategies.PersonSearchStrategy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class PersonsService : IPersonService
    {
        private readonly ICountriesService _countriesService;
        //private IPersonSearchStrategy? _personSearchStrategy;
        //private readonly PersonsDbContext _personsDBContext;
        private readonly ILogger<PersonsService> _logger;
        private readonly IPersonRepository _personRepository;
        private readonly IDiagnosticContext _diagnosticContext;


        public PersonsService(PersonsDbContext db, ICountriesService countriesService, IPersonRepository personRepository, ILogger<PersonsService> logger, IDiagnosticContext diagnosticContext)
        {
            _personRepository = personRepository;
            _countriesService = countriesService;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
            #region Old manual data feed
            /*
            if (initialize)
            {
                _persons.Add(new Person() { PersonID = Guid.Parse("8082ED0C-396D-4162-AD1D-29A13F929824"), Name = "Aguste", Email = "aleddy0@booking.com", Dob = DateTime.Parse("1993-01-02"), Gender = "Male", Address = "0858 Novick Terrace", ReceiveNewsLetter = false, CountryId = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B") });

                _persons.Add(new Person() { PersonID = Guid.Parse("06D15BAD-52F4-498E-B478-ACAD847ABFAA"), Name = "Jasmina", Email = "jsyddie1@miibeian.gov.cn", Dob = DateTime.Parse("1991-06-24"), Gender = "Female", Address = "0742 Fieldstone Lane", ReceiveNewsLetter = true, CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F") });

                _persons.Add(new Person() { PersonID = Guid.Parse("D3EA677A-0F5B-41EA-8FEF-EA2FC41900FD"), Name = "Kendall", Email = "khaquard2@arstechnica.com", Dob = DateTime.Parse("1993-08-13"), Gender = "Male", Address = "7050 Pawling Alley", ReceiveNewsLetter = false, CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F") });

                _persons.Add(new Person() { PersonID = Guid.Parse("89452EDB-BF8C-4283-9BA4-8259FD4A7A76"), Name = "Kilian", Email = "kaizikowitz3@joomla.org", Dob = DateTime.Parse("1991-06-17"), Gender = "Male", Address = "233 Buhler Junction", ReceiveNewsLetter = true, CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E") });

                _persons.Add(new Person() { PersonID = Guid.Parse("F5BD5979-1DC1-432C-B1F1-DB5BCCB0E56D"), Name = "Dulcinea", Email = "dbus4@pbs.org", Dob = DateTime.Parse("1996-09-02"), Gender = "Female", Address = "56 Sundown Point", ReceiveNewsLetter = false, CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E") });

                _persons.Add(new Person() { PersonID = Guid.Parse("A795E22D-FAED-42F0-B134-F3B89B8683E5"), Name = "Corabelle", Email = "cadams5@t-online.de", Dob = DateTime.Parse("1993-10-23"), Gender = "Male", Address = "4489 Hazelcrest Place", ReceiveNewsLetter = false, CountryId = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D") });

                _persons.Add(new Person() { PersonID = Guid.Parse("3C12D8E8-3C1C-4F57-B6A4-C8CAAC893D7A"), Name = "Faydra", Email = "fbischof6@boston.com", Dob = DateTime.Parse("1996-02-14"), Gender = "Female", Address = "2010 Farragut Pass", ReceiveNewsLetter = true, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _persons.Add(new Person() { PersonID = Guid.Parse("7B75097B-BFF2-459F-8EA8-63742BBD7AFB"), Name = "Oby", Email = "oclutheram7@foxnews.com", Dob = DateTime.Parse("1992-05-31"), Gender = "Male", Address = "2 Fallview Plaza", ReceiveNewsLetter = false, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _persons.Add(new Person() { PersonID = Guid.Parse("6717C42D-16EC-4F15-80D8-4C7413E250CB"), Name = "Seumas", Email = "ssimonitto8@biglobe.ne.jp", Dob = DateTime.Parse("1999-02-02"), Gender = "Male", Address = "76779 Norway Maple Crossing", ReceiveNewsLetter = false, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB") });

                _persons.Add(new Person()
                {
                    PersonID = Guid.Parse("6E789C86-C8A6-4F18-821C-2ABDB2E95982"), Name ="Freemon", Email = "faugustin9@vimeo.com",
                    Dob = DateTime.Parse("1996-04-27"),Gender = "Male",Address = "8754 Becker Street",ReceiveNewsLetter= false, CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB")
                });
            */
            #endregion
        }

        #region AddPerson
        public async Task<PersonResponseDTO> AddPerson(AddPersonRequestDTO requestDTO)
        {
            //Model Validations
            ValidationHelper.ModelValidation(requestDTO);

            Person person = requestDTO.ToPerson();
            person.PersonID = Guid.NewGuid();
            await _personRepository.AddPerson(person);
            //await _personsDBContext.Persons.AddAsync(person);
            //await _personsDBContext.SaveChangesAsync();
            //_personsDBContext.sp_InsertPerson(person);
            return person.ToPersonResponseDTO();

        }

        public async Task<bool> DeletePerson(Guid? personID)
        {
            if (personID == null)
            {
                throw new ArgumentNullException("Null value");
            }
            //Person person = await _personsDBContext.Persons.FirstOrDefaultAsync(temp => temp.PersonID.Equals(personID));
            Person? person = await _personRepository.GetPersonByPersonId(personID);
            if (person != null)
            {
                await _personRepository.DeletePersonByPersonID(personID);
                return true;
            }
            return false;
        }
        #endregion

        #region GetAllPersons
        public async Task<List<PersonResponseDTO>> GetAllPersons()
        {
            _logger.LogInformation("Get All persons of PersonService");
            var persons = await _personRepository.GetAllPersons();
            return persons.Select(person => person.ToPersonResponseDTO()).ToList();
            //return _personsDBContext.sp_GetAllPersons().Select(person => ConvertPersonToPersonResponseDTO(person)).ToList();

        }
        #endregion

        #region GetFilteredPersons
        public async Task<List<PersonResponseDTO>> GetFilteredPersons(string searchBy, string searchString)
        {
            _logger.LogInformation("Get Filtered person of PersonService");
            List<Person> persons;
            using (Operation.Time("Time for filtered persons from datatabase"))
            {

                persons = searchBy
                    switch
                {
                    nameof(PersonResponseDTO.PersonName) =>
                        await _personRepository.GetFilteredPersons(temp => temp.PersonName.Contains(searchString)),

                    nameof(PersonResponseDTO.Email) =>
                        await _personRepository.GetFilteredPersons(temp =>
                    temp.Email.Contains(searchString)),

                    nameof(PersonResponseDTO.DOB) =>
                     await _personRepository.GetFilteredPersons(temp =>
                     temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(searchString)),

                    nameof(PersonResponseDTO.Gender) =>
                     await _personRepository.GetFilteredPersons(temp =>
                     temp.Gender.Contains(searchString)),

                    nameof(PersonResponseDTO.CountryName) =>
                     await _personRepository.GetFilteredPersons(temp =>
                     temp.Country.CountryName.Contains(searchString)),

                    nameof(PersonResponseDTO.Address) =>
                    await _personRepository.GetFilteredPersons(temp =>
                    temp.Address.Contains(searchString)),

                    _ => await _personRepository.GetAllPersons()
                };
            }
            _diagnosticContext.Set("Persons", persons);
            return persons.Select(temp => temp.ToPersonResponseDTO()).ToList();
        }
        #endregion

        #region GetPersonByID
        public async Task<PersonResponseDTO> GetPersonByID(Guid? id)
        {
            if (string.IsNullOrEmpty(id.Value.ToString()))
                throw new ArgumentNullException("Error ");
            Person person = await _personRepository.GetPersonByPersonId(id);
            if (person == null)
                return null;
            return person.ToPersonResponseDTO();
        }

        public async Task<MemoryStream> GetPersonsCSV()
        {
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter streamWriter = new StreamWriter(memoryStream);
            CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture, leaveOpen: true);

            csvWriter.WriteHeader<PersonResponseDTO>(); //PersonID,PersonName,...
            csvWriter.NextRecord();

            List<PersonResponseDTO> persons = await GetAllPersons();

            await csvWriter.WriteRecordsAsync(persons);
            //1,abc,....

            memoryStream.Position = 0;
            return memoryStream;
        }
        #endregion
        public async Task<MemoryStream> GetPersonsExcel()
        {
            MemoryStream memoryStream = new MemoryStream();
            using (ExcelPackage excelPackage = new ExcelPackage(memoryStream))
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Persons");
                worksheet.Cells["A1"].Value = "Person Name";
                worksheet.Cells["B1"].Value = "Email";
                worksheet.Cells["C1"].Value = "Date_Of_Birth";
                worksheet.Cells["D1"].Value = "Age";
                worksheet.Cells["E1"].Value = "Gender";
                worksheet.Cells["F1"].Value = "Country Name";
                worksheet.Cells["G1"].Value = "Address";
                worksheet.Cells["H1"].Value = "Receive News Letter";

                using (ExcelRange headerCells = worksheet.Cells["A1:H1"])
                {
                    headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                }
                int row = 2;
                List<PersonResponseDTO> personResponseDTOs = await GetAllPersons();
                foreach (var item in personResponseDTOs)
                {
                    worksheet.Cells[row, 1].Value = item.PersonName;
                    worksheet.Cells[row, 2].Value = item.Email;
                    worksheet.Cells[row, 3].Value = item?.DOB.Value.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 4].Value = item.Age;
                    worksheet.Cells[row, 5].Value = item.Gender;
                    worksheet.Cells[row, 6].Value = item.CountryName;
                    worksheet.Cells[row, 7].Value = item.Address;
                    worksheet.Cells[row, 8].Value = item.ReceiveNewsLetter;
                    row++;
                }
                worksheet.Cells[$"A1:H{row}"].AutoFitColumns();
                await excelPackage.SaveAsync();
            }
            memoryStream.Position = 0;
            return memoryStream;
        }


        #region GetSortedPersons
        public async Task<List<PersonResponseDTO>> GetSortedPersons(List<PersonResponseDTO> filteredPersons, string sortBy, SortOrderOptionType sortOrderOptionType)
        {

            _logger.LogInformation($"--------------------{nameof(GetSortedPersons)}--------------------------");
            List<PersonResponseDTO> sortedPersons = (sortBy, sortOrderOptionType)
                switch
            {
                (nameof(PersonResponseDTO.PersonName), SortOrderOptionType.Ascending) =>
                    filteredPersons.OrderBy(person => person.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponseDTO.PersonName), SortOrderOptionType.Descending) =>
                    filteredPersons.OrderByDescending(person => person.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponseDTO.Email), SortOrderOptionType.Ascending) =>
                    filteredPersons.OrderBy(person => person.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponseDTO.Email), SortOrderOptionType.Descending) =>
                    filteredPersons.OrderByDescending(person => person.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponseDTO.DOB), SortOrderOptionType.Ascending) =>
                filteredPersons.OrderBy(person => person.DOB).ToList(),
                (nameof(PersonResponseDTO.DOB), SortOrderOptionType.Descending) =>
                    filteredPersons.OrderByDescending(person => person.DOB).ToList(),

                (nameof(PersonResponseDTO.Age), SortOrderOptionType.Ascending) =>
                    filteredPersons.OrderBy(person => person.DOB).ToList(),
                (nameof(PersonResponseDTO.Age), SortOrderOptionType.Descending) =>
                    filteredPersons.OrderByDescending(person => person.DOB).ToList(),

                (nameof(PersonResponseDTO.Gender), SortOrderOptionType.Ascending) =>
                    filteredPersons.OrderBy(person => person.Gender.ToString(), StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponseDTO.Gender), SortOrderOptionType.Descending) =>
                    filteredPersons.OrderByDescending(person => person.Gender.ToString(), StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponseDTO.Address), SortOrderOptionType.Ascending) =>
                    filteredPersons.OrderBy(person => person.Address, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponseDTO.Address), SortOrderOptionType.Descending) =>
                    filteredPersons.OrderByDescending(person => person.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponseDTO.CountryName), SortOrderOptionType.Ascending) =>
                    filteredPersons.OrderBy(person => person.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(PersonResponseDTO.CountryName), SortOrderOptionType.Descending) =>
                    filteredPersons.OrderByDescending(person => person.CountryName, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponseDTO.ReceiveNewsLetter), SortOrderOptionType.Ascending) =>
                    filteredPersons.OrderBy(person => person.ReceiveNewsLetter).ToList(),
                (nameof(PersonResponseDTO.ReceiveNewsLetter), SortOrderOptionType.Descending) =>
                    filteredPersons.OrderByDescending(person => person.ReceiveNewsLetter).ToList(),
                _ => filteredPersons
            };
            return sortedPersons;
        }
        #endregion

        #region UpdatePerson
        public async Task<PersonResponseDTO> UpdatePerson(UpdatePersonRequestDTO requestDTO)
        {
            if (requestDTO == null)
                throw new ArgumentNullException(nameof(requestDTO));

            ValidationHelper.ModelValidation(requestDTO);

            Person? matchingPeron = await _personRepository.UpdatePerson(requestDTO.ToPerson());

            return matchingPeron.ToPersonResponseDTO();
        }
        #endregion

        //#region ConvertPersonToPersonResponseDTO
        ///// <summary>
        ///// This method is to convert person domain model to person respons dto with fetching country name.
        ///// </summary>
        ///// <param name="person"></param>
        ///// <returns></returns>
        //private PersonResponseDTO ConvertPersonToPersonResponseDTO(Person person)
        //{
        //    PersonResponseDTO responseDTO = person.ToPersonResponseDTO();
        //    responseDTO.CountryName = person.Country?.CountryName;
        //    return responseDTO;
        //}
        //#endregion
    }
}
