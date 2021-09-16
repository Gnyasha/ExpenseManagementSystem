using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Models
{
    public class Transaction
    {
        public virtual int Id { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual string Narration { get; set; }
        public virtual string Reference { get; set; }
        public virtual int DebitAccountId { get; set; }
        public virtual int CreditAccountId { get; set; }
        public virtual decimal Debit { get; set; }
        public virtual decimal Credit { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual int TransactionStatusId { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual string ModifiedBy { get; set; }
        public virtual int TransactionTypeId { get; set; }
    }
}
