using Demo.Infrastructure.Database;
using Demo.Infrastructure.EntityConfigurations;
using Demo.Migrations.Conventions;
using Demo.Migrations.Migrations;
using FluentMigrator.Runner;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using NLog.Extensions.Logging;
using ISession = NHibernate.ISession;

namespace Demo.Infrastructure
{
    public class NHibernateHelper : INHibernateHelper
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
                .Database(OracleManagedDataClientConfiguration.Oracle10
                    .ConnectionString(_connectionString)
                    .Driver<NHibernate.Driver.OracleManagedDataClientDriver>())
                .Mappings(m =>
                {
                    m.FluentMappings.Add<ProductTypeMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<ProductMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<AddressMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<CompanyMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<AddressCompanyMap>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<Address2Map>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<Company2Map>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                    m.FluentMappings.Add<AddressCompany2Map>().Conventions.AddFromAssemblyOf<LowercaseTableNameConvention>();
                });

#if DEBUG
            fluentConfig.ExposeConfiguration(cfg =>
            {
                var serviceProvider = CreateServices(_connectionString);

                using (var scope = serviceProvider.CreateScope())
                {
                    UpdateDatabase(scope.ServiceProvider, null);
                }
                //new SchemaExport(cfg)
                //    //.SetOutputFile("schema.sql")
                //    //UpdateDatabase
                //    .Execute(true, true, false);
            });
#else
            fluentConfig.ExposeConfiguration(cfg =>
                new SchemaExport(cfg)
                    .SetOutputFile("schema.sql")
                    .SetDelimiter(";")
                    .Execute(false, false, false));
#endif

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
                    .AddOracle12CManaged()
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
