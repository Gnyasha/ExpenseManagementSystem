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
    
    public class SystemUserDao : Contracts.Data.ISystemUserDao
    {
        private readonly ISession m_Session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public SystemUserDao(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            m_Session = session;
        }

        /// <summary>
        /// Returns a list of SystemUsers
        /// </summary>
        /// <returns></returns>
        public IQueryable<SystemUser> GetAllSystemUsers()
        {
            return m_Session.Query<SystemUser>();
        }

        /// <summary>
        /// Saves or updates a person entity and returns the same person entity
        /// </summary>
        /// <param name="systemUser"></param>
        /// <returns></returns>
        public SystemUser SaveOrUpdatePerson(SystemUser systemUser)
        {
            m_Session.SaveOrUpdate(systemUser);
            return systemUser;
        }



    }
}
