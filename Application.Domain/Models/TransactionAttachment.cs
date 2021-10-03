using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Models
{
    public class TransactionAttachment
    {
        public virtual int Id { get; set; }
        public virtual string AttachmentName { get; set; }
        public virtual string TransactionReference { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual byte[] Attachment { get; set; }

    }
}
