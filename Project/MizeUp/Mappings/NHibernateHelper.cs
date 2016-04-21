using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MizeUP.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MizeUP.Mappings
{
    public class NHibernateHelper
    {
        private static ISessionFactory sessionFactory;

        public static ISession OpenSession()
        {
            sessionFactory = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString("Server=localhost; port=5432; database=mizeup; User ID=postgres; Password=mizeup;"))
                //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
