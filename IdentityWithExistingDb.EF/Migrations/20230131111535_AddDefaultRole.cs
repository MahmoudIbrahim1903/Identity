using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityWithExistingDb.EF.Migrations
{
    public partial class AddDefaultRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Roles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES (NEWID(), 'Default', 'DEFAULT', NEWID())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Roles WHERE Name = 'Default'");
        }
    }
}
