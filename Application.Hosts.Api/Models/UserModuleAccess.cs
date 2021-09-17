using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Hosts.Api.Models
{
    public class UserModuleAccess
    {
      
        public int RoleId { get; set; }
        public bool active { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    
    }
}
