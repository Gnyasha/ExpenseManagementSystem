using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Hosts.Api.Models
{
    public class TransactionModel
    {
        public string Reference { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string Narration { get; set; }

        [Required]
        public decimal Amount { get; set; }

    }


}
