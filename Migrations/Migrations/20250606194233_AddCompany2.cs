using Demo.Migrations.Conventions;
using FluentMigrator;

namespace Demo.Migrations.Migrations
{
    [Migration(20250606194233)]
    public class AddCompany2 : Migration
    {
        private readonly string TableName = LowercaseTableNameConvention.TablePrefix + "Company2";

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Version").AsInt32()
                .WithColumn("Foo").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}