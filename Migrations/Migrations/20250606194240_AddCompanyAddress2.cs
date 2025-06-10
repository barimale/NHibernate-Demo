using Demo.Migrations.Conventions;
using FluentMigrator;

namespace Demo.Migrations.Migrations
{
    [Migration(20250606194240)]
    public class AddCompanyAddress2 : Migration
    {
        private readonly string TableName = LowercaseTableNameConvention.TablePrefix + "CompanyAddress2";
        private readonly string CompanyTable = LowercaseTableNameConvention.TablePrefix + "Company2";
        private readonly string AddressTable = LowercaseTableNameConvention.TablePrefix + "Address2";

        public override void Up()
        {
            Create.Table(TableName)
                .WithColumn("Id").AsInt32().NotNullable().Identity()
                .WithColumn("CompanyId").AsInt32().NotNullable()
                .WithColumn("AddressId").AsInt32().NotNullable();

            Create.PrimaryKey("PK_CompanyAddress2")
                .OnTable(TableName)
                .Columns("CompanyId", "AddressId");

            Create.ForeignKey("FK_CompanyAddress2_Address")
                .FromTable(TableName).ForeignColumn("AddressId")
                .ToTable(AddressTable).PrimaryColumn("Id");

            Create.ForeignKey("FK_CompanyAddress2_Company")
                .FromTable(TableName).ForeignColumn("CompanyId")
                .ToTable(CompanyTable).PrimaryColumn("Id");
        }

        public override void Down()
        {
            Delete.Table(TableName);
        }
    }
}