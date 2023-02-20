using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityWithExistingDb.EF.Migrations
{
    public partial class AssignAdminRoleToAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"INSERT INTO UserRoles (UserId, RoleId) 
                                   Values ((SELECT TOP 1 Id FROM Users WHERE UserName = 'Admin'), (SELECT TOP 1 Id FROM Roles WHERE Name = 'Admin'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM UserRoles
                                   WHERE UserId = (SELECT TOP 1 Id FROM Users WHERE UserName = 'Admin')
                                   AND RoleId = (SELECT TOP 1 Id FROM Roles WHERE Name = 'Admin')");
        }
    }
}
