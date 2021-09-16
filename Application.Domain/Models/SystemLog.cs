using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Models
{
    public class SystemLog
    {
        public virtual int Id { get; set; }
        public virtual string LogType { get; set; }
        public virtual string Details { get; set; }
        public virtual string Params { get; set; }
        public virtual DateTime DateCreated { get; set; }
    }
}
