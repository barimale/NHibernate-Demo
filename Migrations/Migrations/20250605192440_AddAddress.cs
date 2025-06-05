using FluentMigrator;
using Migrations.Conventions;

namespace Migrations.Migrations
{
    [Migration(20250605192440)]
    public class AddAddress : Migration
    {
        public override void Up()
        {
            Create.Table(LowercaseTableNameConvention.TablePrefix + "Address")
             .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
             .WithColumn("Street").AsString()
             .WithColumn("City").AsString()
             .WithColumn("State").AsString()
             .WithColumn("ZipCode").AsString()
             .WithColumn("Country").AsString()
             .WithColumn("Phone").AsString();
        }

        public override void Down()
        {
            Delete.Table(LowercaseTableNameConvention.TablePrefix + "Address");
        }
    }
}
