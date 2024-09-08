using Chat_Service.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat_Service.Persistence.Users
{
    public class ApplicationUsersEntityTypeConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.FirstName).HasMaxLength(20);
            builder.Property(x => x.LastName).HasMaxLength(30);
            builder.Property(x => x.UserName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(40);
            builder.Property(x => x.State).HasMaxLength(20);
            builder.Property(x => x.Country).HasMaxLength(20);
            builder.Property(x => x.IsActive).IsRequired();
            builder.Property(x => x.FirstTimeLogin).IsRequired();
            builder.Property(x => x.NormalizedEmail).HasMaxLength(40);
            builder.Property(x => x.NormalizedUserName).HasMaxLength(40).IsRequired();
            builder.Property(x => x.PasswordHash).HasMaxLength(100);
            builder.Property(x => x.PhoneNumber).HasMaxLength(20);
            builder.Property(x => x.SecurityStamp).HasMaxLength(100);
            builder.Property(x => x.ConcurrencyStamp).HasMaxLength(100);
        }
    }
}
