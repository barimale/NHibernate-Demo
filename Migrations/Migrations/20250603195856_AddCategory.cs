using FluentMigrator;

namespace Migrations.Migrations
{
    [Migration(20250603195856)]
    public class AddCategory : Migration
    {
        public override void Up()
        {
            Create.Table("Categories")
                 .WithColumn("Id").AsInt32().NotNullable().PrimaryKey()
                 .WithColumn("Name").AsString();
            Execute.Sql("INSERT INTO Categories SELECT DISTINCT Category FROM Products");
            Alter.Table("Products")
            .AddColumn("CategoryId")
            .AsInt32()
            .Nullable();
            Execute.Sql("UPDATE p SET p.CategoryId = (SELECT c.Id FROM Categories c WHERE c.Name = p.Category) FROM Products p");
            Alter.Column("CategoryId")
            .OnTable("Products")
            .AsInt32()
            .NotNullable()
            .ForeignKey()
            .Indexed();
            Delete.Column("Category")
            .FromTable("Products");
        }

        public override void Down()
        {
            Alter.Table("Products")
             .AddColumn("Category")
             .AsString()
             .Nullable();
            Execute.Sql("UPDATE p SET p.Category = (SELECT c.Name FROM Categories c WHERE c.Id = p.CategoryId) FROM Products p");
            Alter.Column("Category")
            .OnTable("Products")
            .AsString()
            .NotNullable();
            Delete.ForeignKey()
            .FromTable("Products")
            .ForeignColumn("CategoryId")
            .ToTable("Categories")
            .PrimaryColumn("Id");
            Delete.Index()
            .OnTable("Products")
            .OnColumn("CategoryId");
            Delete.Column("CategoryId")
            .FromTable("Products");
            Delete.Table("Categories");
        }
    }
}
