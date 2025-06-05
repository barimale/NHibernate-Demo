using FluentMigrator;
using Migrations.Conventions;

namespace Migrations.Migrations
{
    [Migration(20250605193138)]
    public class AddCompany : Migration
    {
        private const string NAME = "Company";
        public override void Up()
        {
            Create.Table(LowercaseTableNameConvention.TablePrefix + NAME)
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity();
        }

        public override void Down()
        {
            Delete.Table(LowercaseTableNameConvention.TablePrefix + NAME);
        }
    }
}
