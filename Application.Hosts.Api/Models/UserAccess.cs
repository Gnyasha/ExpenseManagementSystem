using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Hosts.Api.Models
{
    public static class UserAccess
    {
        public static class Role
        {
            public const string Admin = "Admin";
            public const string User = "User";
            public const string Basic = "Basic";
        }
        public enum UserRoles
        {
            Admin = 1,
            User = 2,
            Basic = 3,
        }
    }
}
