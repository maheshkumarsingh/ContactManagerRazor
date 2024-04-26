using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Person
    {
        private Guid _personID;
        private string? _name;
        private string? _email;
        private DateTime? _dob;
        private string _gender;
        private Guid? _countryId;
        private string? _address;
        private bool? _receiveNewsLetter;

        [Key]
        public Guid PersonID { get => _personID; set => _personID = value; }
        [StringLength(40)]
        public string? PersonName { get => _name; set => _name = value; }
        [StringLength(40)]
        public string? Email { get => _email; set => _email = value; }
        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get => _dob; set => _dob = value; }
        public string Gender { get => _gender; set => _gender = value; }
        public Guid? CountryID { get => _countryId; set => _countryId = value; }
        [StringLength(100)]
        public string? Address { get => _address; set => _address = value; }
        
        public bool? ReceiveNewsLetters { get => _receiveNewsLetter; set => _receiveNewsLetter = value; }
        public string? TIN { get; set; }

        [ForeignKey("CountryID")]
        public virtual Country? Country { get; set; }


        public override string ToString()
        {
            return $"{PersonID},  {PersonName},  {Email},  {DateOfBirth},  {Gender},  {CountryID},  {Country?.CountryName}, {Address},  {ReceiveNewsLetters}";
        }
    }
}
