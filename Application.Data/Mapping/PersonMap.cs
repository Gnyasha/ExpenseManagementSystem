using System;
using System.Collections.Generic;
using System.Text;

/*Nuget dependancies*/
using FluentNHibernate.Mapping;

namespace Application.Data.Mapping
{
    using Application.Domain.Models;

    public class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Id(p => p.Id);
            Map(p => p.FirstName);
            Map(p => p.MiddleName);
            Map(p => p.LastName);
            Map(p => p.DateOfBirth);
            Map(p => p.Email);
            Table("Persons");
        }

    }
}
