using CommandLine;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Migrations.Migrations;
using NLog.Extensions.Logging;

namespace FluentMigratorExample.Migrator
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default.ParseArguments<Options>(args);

            result
                .WithParsed(r => Migrate(r));
        }

        private static void Migrate(Options options)
        {
            var serviceProvider = CreateServices(options.ConnectionString);

            using (var scope = serviceProvider.CreateScope())
            {
                UpdateDatabase(scope.ServiceProvider, options);
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

        private static void UpdateDatabase(IServiceProvider serviceProvider, Options options)
        {
            var runner = serviceProvider.GetRequiredService<IMigrationRunner>();
            
            if (options.Version.HasValue)
            {
                runner.MigrateDown(options.Version.Value);
            }
            else
            {
                runner.MigrateUp();
            }
        }
    }
}