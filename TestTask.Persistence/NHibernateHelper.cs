using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;

namespace TestTask.Persistence
{
    public static class NHibernateHelper
    {
        private static ISessionFactory? _sessionFactory;

        public static ISessionFactory SessionFactory => _sessionFactory ??= CreateSessionFactory();

        private static ISessionFactory CreateSessionFactory()
        {
            try
            {
                return Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString("Server=localhost;Database=testtask;Uid=appuser;Pwd=appuserpass;")
                        .ShowSql()
                )
                .Mappings(m =>
                {
                    var mappingAssembly = Assembly.GetAssembly(typeof(NHibernateHelper));
                    if (mappingAssembly == null)
                    {
                        throw new InvalidOperationException("Failed to load assembly with mappings.");
                    }
                    m.FluentMappings.AddFromAssembly(mappingAssembly);
                })
                .ExposeConfiguration(cfg =>
                {
                    try
                    {
#if DEBUG
                        new SchemaExport(cfg).Drop(false, true);
                        new SchemaExport(cfg).Create(false, true);
#endif
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException("Error creating database schema.", ex);
                    }
                })
                .BuildSessionFactory();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to create NHibernate session factory.", ex);
            }
        }
    }
}