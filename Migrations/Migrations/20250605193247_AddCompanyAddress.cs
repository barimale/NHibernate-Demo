using FluentMigrator;
using Migrations.Conventions;
using System.Xml.Linq;

namespace Migrations.Migrations
{
    [Migration(20250605193247)]
    public class AddCompanyAddress : Migration
    {
        public const string NAME = "CompanyAddress";

        public override void Up()
        {
            Create.Table(LowercaseTableNameConvention.TablePrefix + NAME)
                            //.WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()

            .WithColumn("CompanyId").AsInt32().NotNullable()
            .WithColumn("AddressId").AsInt32().NotNullable()
            .WithColumn("Description").AsString().NotNullable()
            .WithColumn("CreationDate").AsDateTime2().NotNullable();

            Create.PrimaryKey("PK_CompanyAddress").OnTable(LowercaseTableNameConvention.TablePrefix + NAME)
            .Columns("CompanyId", "AddressId");

            Create.ForeignKey("FK_CompanyAddress_Address")
                .FromTable(LowercaseTableNameConvention.TablePrefix + NAME).ForeignColumn("AddressId")
                .ToTable(LowercaseTableNameConvention.TablePrefix + "Address").PrimaryColumn("Id");

            Create.ForeignKey("FK_CompanyAddress_Company")
                .FromTable(LowercaseTableNameConvention.TablePrefix + NAME).ForeignColumn("CompanyId")
                .ToTable(LowercaseTableNameConvention.TablePrefix + "Company").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table(LowercaseTableNameConvention.TablePrefix + NAME);
        }
    }
}
