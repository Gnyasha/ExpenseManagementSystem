using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.Mapping
{
    
    using Application.Domain.Models;
    using FluentNHibernate.Mapping;
    public class TransactionAttachmentMap : ClassMap<TransactionAttachment>
    {
        public TransactionAttachmentMap()
        {
            Id(p => p.Id);
            Map(p => p.DateCreated);
            Map(p => p.AttachmentName);
            Map(p => p.TransactionReference);
            Map(p => p.Attachment);
            Table("TransactionAttachments");
        }

    }
}
