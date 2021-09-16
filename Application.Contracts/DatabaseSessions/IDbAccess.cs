using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts.DatabaseSessions
{
    using Application.Domain.Models;

    public interface IDbAccess
    {
        /// <summary>
        /// Returns all Persons
        /// </summary>
        /// <returns></returns>
        IQueryable<Person> GetAllPersons();
    }
}
