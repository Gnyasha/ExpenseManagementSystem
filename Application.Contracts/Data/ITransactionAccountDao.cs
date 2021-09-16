using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Contracts.Data
{
    using Application.Domain.Models;
    using Application.Contracts.Data.Base;
    using Application.Data.Mapping;
    public interface ITransactionAccountDao : IGenericDao<TransactionAccount>
    {
    }
}
