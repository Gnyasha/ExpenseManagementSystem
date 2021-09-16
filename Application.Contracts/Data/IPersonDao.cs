using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts.Data
{
    using Application.Domain.Models;

    /// <summary>
    /// This ensures that the Person DAO implements these functions
    /// </summary>
    public interface IPersonDao
    {
        IQueryable<Person> GetAllPeople();
        Person GetPersonByEmail(string email);
        Person GetPersonById(string id);
        Person SaveOrUpdatePerson(Person person);
    }
}
