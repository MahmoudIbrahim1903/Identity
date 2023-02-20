using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace IdentityWithExistingDb.Core.Models.Security
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }

    public partial class CustomConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users")
                .Ignore(u => u.AccessFailedCount)
                .Ignore(u => u.LockoutEnabled)
                .Ignore(u => u.LockoutEnd)
                .Ignore(u => u.TwoFactorEnabled);
        }
    }
}
