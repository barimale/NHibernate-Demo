using Demo.Migrations.Conventions;
using FluentMigrator;

namespace Demo.Migrations.Migrations
{
    [Migration(20250614210504)]
    public class AddUserAndRoles : Migration
    {
        public override void Up()
        {
            // Create the Users table
            Create.Table(LowercaseTableNameConvention.TablePrefix + "Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserName").AsString(256).NotNullable().Unique()
                .WithColumn("Email").AsString(256).NotNullable().Unique()
                .WithColumn("PasswordHash").AsString(512).NotNullable();

            Create.UniqueConstraint("UQ_Users_UserName").OnTable(LowercaseTableNameConvention.TablePrefix + "Users").Column("UserName");
            Create.UniqueConstraint("UQ_Users_Email").OnTable(LowercaseTableNameConvention.TablePrefix + "Users").Column("Email");

            // Create the Roles table
            Create.Table(LowercaseTableNameConvention.TablePrefix + "Roles")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(256).NotNullable();

            // Create the join table between Users and Roles
            Create.Table(LowercaseTableNameConvention.TablePrefix + "UserRoles")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                    .ForeignKey("FK_UserRoles_Users", LowercaseTableNameConvention.TablePrefix + "Users", "Id")
                .WithColumn("RoleId").AsInt32().NotNullable()
                    .ForeignKey("FK_UserRoles_Roles", LowercaseTableNameConvention.TablePrefix + "Roles", "Id");

            // Optionally, insert initial seed data
            Insert.IntoTable(LowercaseTableNameConvention.TablePrefix + "Roles").Row(new { Name = "Admin" });
            Insert.IntoTable(LowercaseTableNameConvention.TablePrefix + "Roles").Row(new { Name = "User" });

            Insert.IntoTable(LowercaseTableNameConvention.TablePrefix + "Users").Row(new
            {
                UserName = "superadmin",
                Email = "superadmin@example.com",
                PasswordHash = "hashedpassword1"  // Replace with an actual hash value
            });

            // Once you have the user and role IDs, you could seed the user.roles join table.
            // In practice, you might want to seed these from code after reading the identity records.
            // Here we assume that the inserted user gets an Id of 1 and the Admin role gets an Id of 1.
            Insert.IntoTable(LowercaseTableNameConvention.TablePrefix + "UserRoles").Row(new { UserId = 1, RoleId = 1 });
        }

        public override void Down()
        {
            Delete.Table(LowercaseTableNameConvention.TablePrefix + "UserRoles");
            Delete.Table(LowercaseTableNameConvention.TablePrefix + "Roles");
            Delete.Table(LowercaseTableNameConvention.TablePrefix + "Users");
        }
    }
}
