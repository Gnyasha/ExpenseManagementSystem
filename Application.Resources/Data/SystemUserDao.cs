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

        public void Delete(SystemUser entity)
        {
            m_Session.Delete(entity);
        }

        public IQueryable<SystemUser> GetAll()
        {
            return m_Session.Query<SystemUser>();
        }

        public SystemUser GetById(int id)
        {
            return m_Session.Query<SystemUser>().Where(a => a.Id == id).FirstOrDefault();
        }

        public SystemUser SaveOrUpdate(SystemUser entity)
        {
            m_Session.SaveOrUpdate(entity);
            m_Session.FlushAsync();
            return entity;
        }
    }
}
