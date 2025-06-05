using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Migrations.Conventions;
using Migrations.Migrations;
using NHibernate;
using NLog.Extensions.Logging;
using WebApplication1.Domain;
using ISession = NHibernate.ISession;

namespace WebApplication1.Repositories.DbContext
{
    public class InMemoryNHibernateHelper : INHibernateHelper, IDisposable
    {
        private string _connectionString = "Data Source=CustomDatabaseName;Mode=Memory;Cache=Shared;";
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
                .Database(SQLiteConfiguration.Standard
                    .InMemory()
                    .ConnectionString(_connectionString)
                    .Driver<NHibernate.Driver.SQLite20Driver>()
                    .Dialect<NHibernate.Dialect.SQLiteDialect>())
                .Mappings(m =>
                {
                    m.FluentMappings.Add<ProductTypeMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<ProductMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<AddressMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<CompanyMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<AddressCompanyMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                });
            fluentConfig.ExposeConfiguration(cfg =>
            {
                var serviceProvider = CreateServices(_connectionString);

                using (var scope = serviceProvider.CreateScope())
                {
                    UpdateDatabase(scope.ServiceProvider, null);
                }
            });

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

        private static IServiceProvider CreateServices(string connectionString)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSQLite()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(InitialMigration).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole().AddNLog())
                .BuildServiceProvider(true);
        }

        private static void UpdateDatabase(IServiceProvider serviceProvider, long? version)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            if (version.HasValue)
            {
                runner.MigrateDown(version.Value);
            }
            else
            {
                runner.MigrateUp();
            }
        }
    }
}
