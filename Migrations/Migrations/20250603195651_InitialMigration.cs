using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(20250603195651)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Products")
             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
             .WithColumn("Name").AsString()
             .WithColumn("Category").AsString();
            Insert.IntoTable("Products").Row(new { Name = "Product 1.1", Category = "Category 1", Id = 1 });
            Insert.IntoTable("Products").Row(new { Name = "Product 1.2", Category = "Category 1", Id = 2 });
            Insert.IntoTable("Products").Row(new { Name = "Product 1.3", Category = "Category 1", Id = 3 });
            Insert.IntoTable("Products").Row(new { Name = "Product 2.1", Category = "Category 2", Id = 4 });
            Insert.IntoTable("Products").Row(new { Name = "Product 2.2", Category = "Category 2", Id = 5 });
        }

        public override void Down()
        {
            Delete.Table("Products");
        }
    }
}
