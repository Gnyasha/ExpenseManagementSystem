using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Data.Mapping
{
    using Application.Domain.Models;
    using FluentNHibernate.Mapping;

    public class SystemLogMap : ClassMap<SystemLog>
    {
        public SystemLogMap()
        {
            Id(p => p.Id);
            Map(p => p.DateCreated);
            Map(p => p.Details);
            Map(p => p.LogType);
            Map(p => p.Params);
            Table("SystemLogs");
        }

    }
}
