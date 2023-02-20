using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityWithExistingDb.EF.Migrations
{
    public partial class AddAdminRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Roles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (NEWID(), 'Admin', 'ADMIN', NEWID())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Roles");
        }
    }
}
