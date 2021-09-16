using System;
using System.Collections.Generic;
using System.Text;

/*Nuget Dependancies*/
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Application.Data
{

    using Application.Data.Mapping;

    /// <summary>
    /// Builds NHibernate session factories
    /// </summary>
    public class SessionFactoryBuilder
    {

        /// <summary>
        /// Builds a new NHibernate session factory
        /// </summary>
        /// <param name="connectionString">
        /// The SQL connection string to use to establish the session factory.
        /// </param>
        /// <returns>
        /// A newly created session factory.
        /// </returns>
        public ISessionFactory BuildSessionFactory(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) throw new ArgumentNullException(nameof(connectionString));

            var config = Fluently.Configure();
            config.Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString));
            config.Mappings(m => m.FluentMappings.AddFromAssemblyOf<SystemUserMap>());

            return config.BuildSessionFactory();
        }
    }
}
