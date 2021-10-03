using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Hosts.Api.Models
{
    public class BillAttachmentModel
    {
        [Required]
        public string TransactionReference { get; set; }

        [Required]
        public string AttachmentName { get; set; }

        [Required]
        public IList<IFormFile> Attachments { get; set; }
    }
}
