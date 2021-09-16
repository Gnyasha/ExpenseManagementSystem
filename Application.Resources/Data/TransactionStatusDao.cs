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

    public class TransactionStatusDao : ITransactionStatusDao
    {
        private readonly ISession m_Session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public TransactionStatusDao(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            m_Session = session;
        }

        public void Delete(TransactionStatus entity)
        {
            m_Session.Delete(entity);
        }

        public IReadOnlyList<TransactionStatus> GetAll()
        {
            return (IReadOnlyList<TransactionStatus>)m_Session.Query<TransactionStatus>();
        }

        public TransactionStatus GetById(int id)
        {
            return m_Session.Query<TransactionStatus>().Where(a => a.Id == id).FirstOrDefault();
        }

        public TransactionStatus SaveOrUpdate(TransactionStatus entity)
        {
            m_Session.SaveOrUpdate(entity);
            m_Session.FlushAsync();
            return entity;
        }
    }
}
