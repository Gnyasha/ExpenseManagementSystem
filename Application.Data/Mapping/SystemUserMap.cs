using System;
using System.Collections.Generic;
using System.Text;

/*Nuget dependancies*/
using FluentNHibernate.Mapping;

namespace Application.Data.Mapping
{
    using Application.Domain.Models;

    public class SystemUserMap : ClassMap<SystemUser>
    {
        public SystemUserMap()
        {
            Id(p => p.Id);
            Map(p => p.UserName);
            Map(p => p.Email);
            Map(p => p.FirstName);
            Map(p => p.LastName);
            Map(p => p.PasswordHash);
            Map(p => p.PasswordSalt);
            Map(p => p.DateCreated);
            Map(p => p.IsActive);
            Map(p => p.RoleId);
            Table("SystemUsers");
        }

    }
}
