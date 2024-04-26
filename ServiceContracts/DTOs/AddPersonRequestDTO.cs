using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTOs
{
    /// <summary>
    /// Acts as DTO for adding a person
    /// </summary>
    public class AddPersonRequestDTO
    {
        [Required(ErrorMessage ="Your parents must have named you 😎")]
        [StringLength(50),RegularExpression("^[a-zA-Z0-9\\s]*$")]
        public string? PersonName { get; set; }

        [Required(ErrorMessage ="Email cant be blank")]
        [EmailAddress(ErrorMessage ="Email address should be valid")]
        [DataType(DataType.EmailAddress)]   
        public string? Email { get; set; }
        [Required(ErrorMessage ="You are born right! Fill DOB 😎")]
        [DataType(DataType.Date)]
        public DateTime? DOB { get; set; }

        [Required(ErrorMessage ="Dont Miss it 😎")]
        public string Gender { get; set; }
        public Guid? CountryID { get; set; }
        [Required(ErrorMessage = "Do you live in streets!!")]
        public string? Address { get; set; }
        [Required(ErrorMessage = "Dont try to miss my mails")]
        public bool? RecieveNewsLetter { get; set; }


        public Person ToPerson()
        {
            return new Person()
            {
                PersonName = PersonName,
                Email = Email,
                DateOfBirth = DOB,
                Gender = Gender,
                CountryID = CountryID,
                Address = Address,
                ReceiveNewsLetters = RecieveNewsLetter
            };
        }

    }
}
