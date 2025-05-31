using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using WebApplication1.Domain;
using ISession = NHibernate.ISession;

namespace NHibernateTestBlog
{
    public class NHibernateHelper : INHibernateHelper, IDisposable
    {
        private static readonly object _lock = new();
        private ISessionFactory? _sessionFactory;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private bool _disposed;

        public NHibernateHelper(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("OracleDB") 
                ?? throw new InvalidOperationException("Connection string 'OracleDB' not found.");
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
                            _sessionFactory = Fluently.Configure()
                                .Database(OracleManagedDataClientConfiguration.Oracle10
                                    .ConnectionString(_connectionString)
                                    .Driver<NHibernate.Driver.OracleManagedDataClientDriver>())
                                .Mappings(m =>
                                {
                                    m.FluentMappings.Add<ProductTypeMap>();
                                    m.FluentMappings.Add<ProductMap>();
                                })
#if DEBUG
                                // Remove SchemaExport in production; use only for development/testing
                                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                    .Execute(false, false, false))
#else
                                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                    .SetOutputFile("schema.sql")
                                    .Execute(false, false, false))
#endif
                                .BuildSessionFactory();
                        }
                    }
                }
                return _sessionFactory;
            }
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
