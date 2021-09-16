using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.Mapping
{
    using Application.Domain.Models;
    using FluentNHibernate.Mapping;

    public class TransactionAccountMap : ClassMap<TransactionAccount>
    {
        public TransactionAccountMap()
        {
            Id(p => p.Id);
            Map(p => p.DateCreated);
            Map(p => p.AccountName);
            Map(p => p.BalanceTypeId);
            Map(p => p.DateCreated);
            Map(p => p.CreatedBy);
            Map(p => p.ModifiedBy);
            Table("TransactionAccounts");
        }

    }
}
