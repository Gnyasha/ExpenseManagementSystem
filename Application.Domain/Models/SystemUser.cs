using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Models
{
    public class SystemUser
    {
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual int RoleId { get; set; }

    }
}
