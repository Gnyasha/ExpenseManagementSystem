using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Domain.Models
{
    public class Person
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual DateTime DateOfBirth { get; set; }

    }
}
