using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

public static class NHibernateHelper
{
    public static ISessionFactory CreateSessionFactory(string connectionString)
    {
        return Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
            //.ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, false))
            .BuildSessionFactory();
    }
}
