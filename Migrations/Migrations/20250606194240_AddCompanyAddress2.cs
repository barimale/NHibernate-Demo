using Demo.Migrations.Conventions;
using FluentMigrator;

namespace Demo.Migrations.Migrations
{
    [Migration(20250606194240)]
    public class AddCompanyAddress2 : Migration
    {
        public const string NAME = "CompanyAddress2";

        public override void Up()
        {
            Create.Table(LowercaseTableNameConvention.TablePrefix + NAME)
            .WithColumn("Id").AsInt32().NotNullable().Identity()
            .WithColumn("CompanyId").AsInt32().NotNullable()
            .WithColumn("AddressId").AsInt32().NotNullable();

            Create.PrimaryKey("PK_CompanyAddress2").OnTable(LowercaseTableNameConvention.TablePrefix + NAME)
            .Columns("CompanyId", "AddressId");

            Create.ForeignKey("FK_CompanyAddress2_Address")
                .FromTable(LowercaseTableNameConvention.TablePrefix + NAME).ForeignColumn("AddressId")
                .ToTable(LowercaseTableNameConvention.TablePrefix + "Address2").PrimaryColumn("Id");

            Create.ForeignKey("FK_CompanyAddress2_Company")
                .FromTable(LowercaseTableNameConvention.TablePrefix + NAME).ForeignColumn("CompanyId")
                .ToTable(LowercaseTableNameConvention.TablePrefix + "Company2").PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table(LowercaseTableNameConvention.TablePrefix + NAME);

        }
    }
}
