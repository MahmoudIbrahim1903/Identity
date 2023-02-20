using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityWithExistingDb.EF.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @" INSERT INTO Users 
                (Id, FirstName, LastName, Age, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, PhoneNumber, PhoneNumberConfirmed) 
                VALUES
                (NEWID(), 'Admin', 'Admin', 0, 'Admin', 'ADMIN', 'Admin@Project.eg', 'ADMIN@PROJECT.EG', 1, 'AQAAAAEAACcQAAAAECOOP0DkxoL0PwRX3AGViyfkYt1hmYErohG76QWAs519YXktQILKJqLLragfjfz4hw==', 'RBIF2EQ7NQXOCQPTPXEDI4JWNMLMYQJO', '561d25a6-00d5-46c9-ac87-36214791acff', NULL, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users WHERE UserName = 'Admin'");
        }
    }
}
