using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.Mapping
{
    using Application.Domain.Models;
    using FluentNHibernate.Mapping;
  
    public class TransactionStatusMap : ClassMap<TransactionStatus>
    {
        public TransactionStatusMap()
        {
            Id(p => p.Id);
            Map(p => p.Status);
            Table("TransactionStatus");
        }

    }
}
