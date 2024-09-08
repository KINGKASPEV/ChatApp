using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Chat_Service.Domain.Users;
using Chat_Service.Domain.Common;
using Chat_Service.Application.Interfaces.Persisitence;
using Chat_Service.Domain.ChatEntities;

namespace Chat_Service.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Chat> Chats { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<Entity> entity in ChangeTracker.Entries<Entity>())
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        entity.Entity.DateCreated = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.Entity.DateModified = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and keys
            modelBuilder.Entity<GroupUser>()
                .HasKey(gu => new { gu.GroupId, gu.UserId });

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.Group)
                .WithMany(g => g.GroupUsers)
                .HasForeignKey(gu => gu.GroupId);

            modelBuilder.Entity<GroupUser>()
                .HasOne(gu => gu.User)
                .WithMany(u => u.GroupMemberships)
                .HasForeignKey(gu => gu.UserId);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Sender)
                .WithMany(u => u.SentChats)
                .HasForeignKey(c => c.SenderId);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Receiver)
                .WithMany(u => u.ReceivedChats)
                .HasForeignKey(c => c.ReceiverId);

            modelBuilder.Entity<Chat>()
                .HasOne(c => c.Group)
                .WithMany(g => g.Chats)
                .HasForeignKey(c => c.GroupId);
        }

        public async Task<bool> SaveAllChangesAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(cancellationToken) > 0;
        }
    }
}
