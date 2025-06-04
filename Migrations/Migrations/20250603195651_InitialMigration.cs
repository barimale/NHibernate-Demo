using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(20250603195651)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("tbl_Product")
             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
             .WithColumn("Name").AsString()
             .WithColumn("Category").AsString()
             .WithColumn("Discontinued").AsBoolean().WithDefaultValue(false);

            Insert.IntoTable("tbl_Product").Row(new { Name = "Product 1.1", Category = "Category 1" });
            Insert.IntoTable("tbl_Product").Row(new { Name = "Product 1.2", Category = "Category 1" });
            Insert.IntoTable("tbl_Product").Row(new { Name = "Product 1.3", Category = "Category 1" });
            Insert.IntoTable("tbl_Product").Row(new { Name = "Product 2.1", Category = "Category 2" });
            Insert.IntoTable("tbl_Product").Row(new { Name = "Product 2.2", Category = "Category 2" });
        }

        public override void Down()
        {
            Delete.Table("tbl_Product");
        }
    }
}
