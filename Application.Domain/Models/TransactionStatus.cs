using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Models
{
    public class TransactionStatus
    {
        public virtual int Id { get; set; }
        public virtual string Status { get; set; }
    }
}
