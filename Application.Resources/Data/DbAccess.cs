using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*Nuget Dependancies*/
using NHibernate;

namespace Application.Resources.Data
{
    using Application.Contracts.DatabaseSessions;
    using Application.Domain.Models;

    /// <summary>
    /// Provides access to the database
    /// </summary>
    public class DbAccess : IDbAccess
    {
        private readonly ISession session;

        /// <summary>
        /// Initialises an instance of a <see cref="Person"/>
        /// </summary>
        /// <param name="session"></param>
        public DbAccess(ISession _session)
        {
            session = _session;
        }

        public IQueryable<Person> GetAllPersons()
        {
            return session.Query<Person>();
        }
    }
}
