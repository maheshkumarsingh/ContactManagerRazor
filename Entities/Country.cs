using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Country Domain model
    /// </summary>
    public class Country
    {
        [Key]
        private Guid _id;
        private string? _name;

        [Key]
        public Guid CountryID { get => _id; set => _id = value; }
        public string? CountryName { get => _name; set => _name = value; }

        //public virtual ICollection<Person> Persons { get; set; }
    }
}
