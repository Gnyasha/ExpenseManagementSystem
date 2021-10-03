using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


/*Nuget Dependancies*/
using NHibernate;

namespace Application.Resources.Data
{
    using Application.Contracts.Data;
    using Application.Contracts.Data.Base;
    using Application.Domain.Models;

    public class TransactionAttachmentDao : ITransactionAttachmentDao
    {
        private readonly ISession m_Session;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        public TransactionAttachmentDao(ISession session)
        {
            if (session == null)
            {
                throw new ArgumentNullException(nameof(session));
            }
            m_Session = session;
        }


        public void Delete(TransactionAttachment entity)
        {
            m_Session.Delete(entity);
        }

        public IQueryable<TransactionAttachment> GetAll()
        {
            return m_Session.Query<TransactionAttachment>();

        }
      
        public TransactionAttachment SaveOrUpdate(TransactionAttachment entity)
        {
            m_Session.SaveOrUpdate(entity);
            m_Session.FlushAsync();
            return entity;
        }

        IQueryable<TransactionAttachment> IGenericDao<TransactionAttachment>.GetAll()
        {
            return m_Session.Query<TransactionAttachment>();
        }

        TransactionAttachment IGenericDao<TransactionAttachment>.GetById(int id)
        {
            return m_Session.Query<TransactionAttachment>().Where(a => a.Id == id).FirstOrDefault();
        }
    }
}
