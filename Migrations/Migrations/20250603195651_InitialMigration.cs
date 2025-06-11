using Demo.Migrations.Conventions;
using FluentMigrator;

namespace Demo.Migrations.Migrations
{
    [Migration(20250603195651)]
    public class InitialMigration : Migration
    {
        private readonly string TableName = LowercaseTableNameConvention.TablePrefix + "Product";

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Name").AsString()
                .WithColumn("Category").AsString()
                .WithColumn("Type").AsString().Nullable()
                .WithColumn("Version").AsInt32()
                .WithColumn("Discontinued").AsBoolean().WithDefaultValue(false);

            Insert.IntoTable(TableName).Row(new { Name = "Product 1.1", Category = "Category 1", Version = 1 });
            Insert.IntoTable(TableName).Row(new { Name = "Product 1.2", Category = "Category 1", Version = 1 });
            Insert.IntoTable(TableName).Row(new { Name = "Product 1.3", Category = "Category 1", Version = 1 });
            Insert.IntoTable(TableName).Row(new { Name = "Product 2.1", Category = "Category 2", Version = 1 });
            Insert.IntoTable(TableName).Row(new { Name = "Product 2.2", Category = "Category 2", Version = 1 });
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}