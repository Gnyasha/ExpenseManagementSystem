using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Contracts.DatabaseSessions
{
    using Application.Domain.Models;

    public interface IDbAccess
    {

        //Get list of entities
        IQueryable<SystemUser> GetSystemUsers();
        IQueryable<TransactionStatus> GetTransactionStatuses();
        IQueryable<TransactionAccount> GetTransactionAccounts();
        IQueryable<Transaction> GetTransactions();
        IQueryable<SystemLog> GetSystemLogs();


        // Save or Updates entity and returns the same entity
        SystemUser SaveOrUpdate(SystemUser entity);
        TransactionStatus SaveOrUpdate(TransactionStatus entity);
        TransactionAccount SaveOrUpdate(TransactionAccount entity);
        Transaction SaveOrUpdate(Transaction entity);
        SystemLog SaveOrUpdate(SystemLog entity);


        decimal GetBalance();


        //Delete the provided entity
        void DeleteEntity(SystemUser entity);
        void DeleteEntity(TransactionStatus entity);
        void DeleteEntity(TransactionAccount entity);
        void DeleteEntity(Transaction entity);
        void DeleteEntity(SystemLog entity);
    }
}
