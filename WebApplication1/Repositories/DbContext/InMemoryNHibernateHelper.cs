using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using WebApplication1.Conventions;
using WebApplication1.Domain;
using ISession = NHibernate.ISession;

namespace WebApplication1.Repositories.DbContext
{
    public class InMemoryNHibernateHelper : INHibernateHelper, IDisposable
    {
        private static readonly object _lock = new();
        private ISessionFactory? _sessionFactory;
        private bool _disposed;

        public InMemoryNHibernateHelper()
        {
            //intentionally left blank
        }

        private ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    lock (_lock)
                    {
                        if (_sessionFactory == null)
                        {
                            _sessionFactory = BuildSessionFactory();
                        }
                    }
                }
                return _sessionFactory;
            }
        }

        private ISessionFactory BuildSessionFactory()
        {
            var fluentConfig = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                .Mappings(m =>
                {
                    m.FluentMappings.Add<ProductTypeMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<ProductMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<AddressMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<CompanyMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<AddressCompanyMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                });
            fluentConfig.ExposeConfiguration(cfg =>
                           new SchemaExport(cfg)
                               //.SetOutputFile("schema.sql")
                               .Execute(true, true, true));
            return fluentConfig.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _sessionFactory?.Dispose();
                _disposed = true;
            }
        }
    }
}
