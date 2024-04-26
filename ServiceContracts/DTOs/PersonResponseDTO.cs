using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs
{
    public class PersonResponseDTO
    {
        public Guid PersonID { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public DateTime? DOB { get; set; }
        public string? Gender { get; set; }
        public Guid? CountryID { get; set; }
        //public string? CountryName
        //{
        //    get
        //    {
        //        return GetCountryNameByID();
        //    }
        //    set
        //    {
        //        CountryName = value;
        //    }
        //}
        public string? CountryName { get; set; }

        public string? Address { get; set; }
        public bool? ReceiveNewsLetter { get; set; }
        public double? Age { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is PersonResponseDTO dTO &&
                   PersonID.Equals(dTO.PersonID) &&
                   PersonName == dTO.PersonName &&
                   Email == dTO.Email &&
                   DOB == dTO.DOB &&
                   Gender == dTO.Gender &&
                   EqualityComparer<Guid?>.Default.Equals(CountryID, dTO.CountryID) &&
                   CountryName == dTO.CountryName &&
                   Address == dTO.Address &&
                   ReceiveNewsLetter == dTO.ReceiveNewsLetter &&
                   Age == dTO.Age;
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(PersonID);
            hash.Add(PersonName);
            hash.Add(Email);
            hash.Add(DOB);
            hash.Add(Gender);
            hash.Add(CountryID);
            hash.Add(CountryName);
            hash.Add(Address);
            hash.Add(ReceiveNewsLetter);
            hash.Add(Age);
            return hash.ToHashCode();
        }
        public override string ToString()
        {
            return $"Person ID: {this.PersonID}, Person Name: {PersonName}, Email: {Email}, Date of Birth: {DOB?.ToString("dd MMM yyyy")}, Gender: {Gender}, Country ID: {CountryID}, Country: {CountryID}, Address: {Address}";
        }

        public UpdatePersonRequestDTO ToPersonUpdateRequestDTO()
        {
            return new UpdatePersonRequestDTO()
            {
                PersonID = PersonID,
                PersonName = PersonName,
                Email = Email,
                DOB = DOB,
                Gender = Gender,
                CountryID = CountryID,
                Address = Address,
            };
        }
    }

    public static class PersonExtension
    {
        public static PersonResponseDTO ToPersonResponseDTO(this Person person)
        {
            return new PersonResponseDTO()
            {
                PersonID            = person.PersonID,
                PersonName          = person.PersonName,
                Email               = person.Email,
                DOB                 = person.DateOfBirth,
                Gender              = person.Gender,
                CountryID           = person.CountryID,
                CountryName         = person.Country?.CountryName,
                ReceiveNewsLetter   = person.ReceiveNewsLetters,
                Address             = person.Address,
                Age                 = (person.DateOfBirth != null) ? Math.Round((DateTime.Now - person.DateOfBirth.Value).TotalDays / 365.25) : null
            };
        }

    }
}
