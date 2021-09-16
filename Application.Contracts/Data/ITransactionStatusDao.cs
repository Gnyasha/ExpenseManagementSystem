
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Application.Contracts.Data
{
    using Application.Domain.Models;
    using Application.Contracts.Data.Base;
    using Application.Data.Mapping;
    public interface ITransactionStatusDao : IGenericDao<TransactionStatus>
    {
    }
}
