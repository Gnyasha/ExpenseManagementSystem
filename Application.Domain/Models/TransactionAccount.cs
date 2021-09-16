using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Models
{
    public class TransactionAccount
    {
        public virtual int Id { get; set; }
        public virtual string AccountName { get; set; }
        public virtual int BalanceTypeId { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
       
    }
}
