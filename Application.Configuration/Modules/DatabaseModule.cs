using System;
using System.Collections.Generic;
using System.Text;

//Nuget Dependancies
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Logging.Serilog;

namespace Application.Configuration.Modules
{
    using Application.Contracts.Data;
    using Application.Contracts.DatabaseSessions;
    using Application.Data;
    using Application.Resources.Data;

    public class DatabaseModule
    {
        /// <summary>
        /// The connection string for the BOM data model
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The name of the database for the underlying data store
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// The user name to access the underlying data store with
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The user password to access the underlying data store with 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The integrate security property
        /// </summary>
        public bool IntegratedSecurity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public void Register(IServiceCollection builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            RegisterDataComponents(builder);
            RegisterDaos(builder);

        }

        /// <summary>
        /// Registers all data components
        /// </summary>
        /// <param name="builder">
        /// The DI container
        /// </param>
        private void RegisterDataComponents(IServiceCollection builder)
        {
            var ctx = $"Server={Host};Database={DatabaseName};integrated security={IntegratedSecurity.ToString()};User Id={UserName};Password={Password};Pooling=true;Min Pool Size=20";

            // Debugging SQL statements
            NHibernateLogger.SetLoggersFactory(new SerilogLoggerFactory());

            builder.AddSingleton<SessionFactoryBuilder>();
            builder.AddSingleton<ISessionFactory>(x =>
            {
                var sessionFactory = x.GetService<SessionFactoryBuilder>();
                return sessionFactory.BuildSessionFactory(ctx);
            });

            builder.AddTransient<ISession>(x =>
            {
                var factory = x.GetService<ISessionFactory>();
                var session = factory.OpenSession();
                session.FlushMode = FlushMode.Commit;

                return session;
            });


        }

        private void RegisterDaos(IServiceCollection builder)
        {
            builder.AddTransient<IDbAccess, DbAccess>();
            builder.AddTransient<IPersonDao, PersonDao>();
        }
    }


}
