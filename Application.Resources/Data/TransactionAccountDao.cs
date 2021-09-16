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

    public class TransactionAccountDao : ITransactionAccountDao
    {
        private readonly ISession m_Session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public TransactionAccountDao(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            m_Session = session;
        }

        public void Delete(TransactionAccount entity)
        {
            m_Session.Delete(entity);
        }

        public IReadOnlyList<TransactionAccount> GetAll()
        {
            return (IReadOnlyList<TransactionAccount>)m_Session.Query<TransactionAccount>();
        }

        public TransactionAccount GetById(int id)
        {
            return m_Session.Query<TransactionAccount>().Where(a => a.Id == id).FirstOrDefault();
        }

        public TransactionAccount SaveOrUpdate(TransactionAccount entity)
        {
            m_Session.SaveOrUpdate(entity);
            m_Session.FlushAsync();
            return entity;
        }
    }
}
