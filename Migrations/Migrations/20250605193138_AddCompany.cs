using Demo.Migrations.Conventions;
using FluentMigrator;

namespace Demo.Migrations.Migrations
{
    [Migration(20250605193138)]
    public class AddCompany : Migration
    {
        private const string NAME = "Company";
        public override void Up()
        {
            Create.Table(LowercaseTableNameConvention.TablePrefix + NAME)
            .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
             .WithColumn("Version").AsInt32().Nullable()
            .WithColumn("Foo").AsString().Nullable();
        }

        public override void Down()
        {
            Delete.Table(LowercaseTableNameConvention.TablePrefix + NAME);
        }
    }
}
