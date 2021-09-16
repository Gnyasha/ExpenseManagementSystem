using Application.Contracts.Data;
using Application.Domain.Models;
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

    public class SystemLogDao : ISystemLogDao
    {
        private readonly ISession m_Session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public SystemLogDao(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            m_Session = session;
        }

        public void Delete(SystemLog entity)
        {
            m_Session.Delete(entity);
        }

        public IReadOnlyList<SystemLog> GetAll()
        {
            return (IReadOnlyList<SystemLog>)m_Session.Query<SystemLog>();
        }

        public SystemLog GetById(int id)
        {
            return m_Session.Query<SystemLog>().Where(a => a.Id == id).FirstOrDefault();
        }

        public SystemLog SaveOrUpdate(SystemLog entity)
        {
            m_Session.SaveOrUpdate(entity);
            m_Session.FlushAsync();
            return entity;
        }
    }
}
