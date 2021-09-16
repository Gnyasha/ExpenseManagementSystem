using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

/*Nuget Dependancies*/
using NHibernate;


namespace Application.Resources.Data
{
    using Application.Contracts.Data;
    using Application.Domain.Models;
    using Application.Resources.Utilities;
    
    public class PersonDao : IPersonDao
    {
        private readonly ISession m_Session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public PersonDao(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            m_Session = session;
        }

        /// <summary>
        /// Returns a list of People
        /// </summary>
        /// <returns></returns>
        public IQueryable<Person> GetAllPeople()
        {
            return m_Session.Query<Person>();
        }

        /// <summary>
        /// Get a person by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Person GetPersonByEmail(string email)
        {
            if (email == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            var validations = new Validations();

            if (!validations.IsValidEmail(email))
            {
                throw new FormatException("Invalid Email");
            }

            return m_Session.Query<Person>().Where(a => a.Email == email).FirstOrDefault();
        }

        /// <summary>
        /// Get a person by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Person GetPersonById(string id)
        {
            return m_Session.Query<Person>().Where(a => a.Id.ToString() == id).FirstOrDefault();
        }

        /// <summary>
        /// Saves or updates a person entity and returns the same person entity
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public Person SaveOrUpdatePerson(Person person)
        {
            m_Session.SaveOrUpdate(person);
            return person;
        }



    }
}
