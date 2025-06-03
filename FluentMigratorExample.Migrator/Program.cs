using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigratorExample.Migrator;
using Microsoft.Extensions.DependencyInjection;
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
                    .AddOracleManaged()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Migrations.Migrations.InitialMigration).Assembly).For.Migrations())
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