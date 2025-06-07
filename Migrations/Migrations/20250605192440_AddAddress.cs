using Demo.Migrations.Conventions;
using FluentMigrator;

namespace Demo.Migrations.Migrations
{
    [Migration(20250605192440)]
    public class AddAddress : Migration
    {
        public override void Up()
        {
            Create.Table(LowercaseTableNameConvention.TablePrefix + "Address")
             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
             .WithColumn("Street").AsString().Nullable()
             .WithColumn("City").AsString().Nullable()
             .WithColumn("State").AsString().Nullable()
             .WithColumn("ZipCode").AsString().Nullable()
             .WithColumn("Country").AsString().Nullable()
             .WithColumn("Phone").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table(LowercaseTableNameConvention.TablePrefix + "Address");
        }
    }
}
