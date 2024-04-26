using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Repositories
{
    public class PersonsRepository : IPersonRepository
    {
        private readonly PersonsDbContext _context;
        private readonly ILogger<PersonsRepository> _logger;
        public PersonsRepository(PersonsDbContext context,ILogger<PersonsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Person> AddPerson(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeletePersonByPersonID(Guid? guid)
        {
            _context.Persons.RemoveRange(_context.Persons.Where(person => person.PersonID == guid));
            int rowsDeleeted = await _context.SaveChangesAsync();
            return rowsDeleeted > 0;    
        }

        public async Task<List<Person>> GetAllPersons()
        {
            return await _context.Persons.Include("Country").ToListAsync();
        }

        public async Task<List<Person>> GetFilteredPersons(Expression<Func<Person, bool>> predicate)
        {
            await Console.Out.WriteLineAsync("$--------------------\"{MethodBase.GetCurrentMethod().Name}\"---------------------------------");
            _logger.LogInformation($"{MethodBase.GetCurrentMethod().Name}");
            return await _context.Persons.Include("Country").Where(predicate).ToListAsync();
        }

        public async Task<Person?> GetPersonByPersonId(Guid? personId)
        {
            return await _context.Persons.Include("Country").FirstOrDefaultAsync(temp => temp.PersonID == personId);
        }

        public async Task<Person?> UpdatePerson(Person person)
        {
            Person? matchingPerson = await _context.Persons.FirstOrDefaultAsync(temp => temp.PersonID.Equals(person.PersonID));

            if(matchingPerson == null)
            {
                return null;
            }
            matchingPerson.PersonName = person.PersonName;
            matchingPerson.Email = person.Email;
            matchingPerson.Gender = person.Gender;
            matchingPerson.DateOfBirth = person.DateOfBirth;
            matchingPerson.CountryID = person.CountryID;
            matchingPerson.Address = person.Address;
            matchingPerson.ReceiveNewsLetters = person.ReceiveNewsLetters;
            int count = await _context.SaveChangesAsync();
            return matchingPerson;
        }
    }
}
