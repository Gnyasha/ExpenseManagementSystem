using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.Mapping
{
    using Application.Domain.Models;
    using FluentNHibernate.Mapping;
    public class TransactionMap : ClassMap<Transaction>
    {
        public TransactionMap()
        {
            Id(p => p.Id);
            Map(p => p.DateCreated);
            Map(p => p.Narration);
            Map(p => p.Reference);
            Map(p => p.DebitAccountId);
            Map(p => p.CreditAccountId);
            Map(p => p.Debit);
            Map(p => p.Credit);
            Map(p => p.TransactionDate);
            Map(p => p.TransactionStatusId);
            Map(p => p.TransactionTypeId);
            Map(p => p.CreatedBy);
            Map(p => p.ModifiedBy);
            Table("Transactions");
        }

    }
}
