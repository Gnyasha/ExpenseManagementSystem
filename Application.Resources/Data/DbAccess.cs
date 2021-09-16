﻿using System;
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
        /// Initialises an instance of a <see cref="SystemUser"/>
        /// </summary>
        /// <param name="session"></param>
        public DbAccess(ISession _session)
        {
            session = _session;
        }

        public void DeleteEntity(SystemUser entity)
        {
            session.Delete(entity);
        }

        public void DeleteEntity(TransactionStatus entity)
        {
            session.Delete(entity);
        }

        public void DeleteEntity(TransactionAccount entity)
        {
            session.Delete(entity);
        }

        public void DeleteEntity(Transaction entity)
        {
            session.Delete(entity);
        }

        public void DeleteEntity(SystemLog entity)
        {
            session.Delete(entity);
        }

        public IQueryable<TransactionStatus> GetTransactionStatuses()
        {
            return session.Query<TransactionStatus>();
        }

        public IQueryable<SystemLog> GetSystemLogs()
        {
            return session.Query<SystemLog>();
        }

        public IQueryable<SystemUser> GetSystemUsers()
        {
            return session.Query<SystemUser>();
        }

        public IQueryable<TransactionAccount> GetTransactionAccounts()
        {
            return session.Query<TransactionAccount>();
        }

        public IQueryable<Transaction> GetTransactions()
        {
            return session.Query<Transaction>();
        }

        public SystemUser SaveOrUpdate(SystemUser entity)
        {
            session.SaveOrUpdate(entity);
            session.FlushAsync();
            return entity;
        }

        public TransactionStatus SaveOrUpdate(TransactionStatus entity)
        {
            session.SaveOrUpdate(entity);
            session.FlushAsync();
            return entity;
        }

        public TransactionAccount SaveOrUpdate(TransactionAccount entity)
        {
            session.SaveOrUpdate(entity);
            session.FlushAsync();
            return entity;
        }

        public Transaction SaveOrUpdate(Transaction entity)
        {
            session.SaveOrUpdate(entity);
            session.FlushAsync();
            return entity;
        }

        public SystemLog SaveOrUpdate(SystemLog entity)
        {
            session.SaveOrUpdate(entity);
            session.FlushAsync();
            return entity;
        }
    }
}
