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

    public class TransactionDao : ITransactionDao
    {

        private readonly ISession m_Session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public TransactionDao(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            m_Session = session;
        }

        public void Delete(Transaction entity)
        {
            m_Session.Delete(entity);
        }

        public IReadOnlyList<Transaction> GetAll()
        {
            return (IReadOnlyList<Transaction>)m_Session.Query<Transaction>();
        }

        public Transaction GetById(int id)
        {
            return m_Session.Query<Transaction>().Where(a => a.Id == id).FirstOrDefault();
        }

        public Transaction SaveOrUpdate(Transaction entity)
        {
            m_Session.SaveOrUpdate(entity);
            m_Session.FlushAsync();
            return entity;
        }
    }
}
