using Demo.Migrations.Conventions;
using FluentMigrator;

namespace Demo.Migrations.Migrations
{
    [Migration(20250606194233)]
    public class AddCompany2 : Migration
    {
        private const string NAME = "Company2";

        public override void Up()
        {
            Create.Table(LowercaseTableNameConvention.TablePrefix + NAME)
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
            .WithColumn("Foo").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table(LowercaseTableNameConvention.TablePrefix + NAME);

        }
    }
}
