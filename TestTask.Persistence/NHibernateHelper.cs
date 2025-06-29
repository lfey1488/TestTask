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
            return Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString("Server=localhost;Database=testtask;Uid=appuser;Pwd=appuserpass;")
                        .ShowSql()
                )
                .Mappings(m =>
                    m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly())
                )
                .ExposeConfiguration(cfg =>
                {
                    // Создание схемы (таблиц) при запуске
                    new SchemaExport(cfg).Create(false, true);
                })
                .BuildSessionFactory();
        }
    }
}