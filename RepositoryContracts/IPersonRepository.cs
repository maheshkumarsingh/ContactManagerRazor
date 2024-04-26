using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryContracts
{
    public interface IPersonRepository
    {
        Task<Person> AddPerson(Person person);

        Task<List<Person>> GetAllPersons();

        Task<Person?> GetPersonByPersonId(Guid? personId);

        Task<Person?> UpdatePerson(Person person);

        Task<bool> DeletePersonByPersonID(Guid? guid);
        Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate);
    }
}
