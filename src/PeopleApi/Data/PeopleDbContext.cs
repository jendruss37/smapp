using Microsoft.EntityFrameworkCore;

namespace PeopleApi.Data
{
    

    public class PeopleDbContext : DbContext
    {
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<UserFollow> UserFollows { get; set; }


        public PeopleDbContext(DbContextOptions<PeopleDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Biogram)
                    .HasMaxLength(500);

                entity.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.DateOfBirth)
                    .IsRequired(false);

                // Ignore FollowedUsers navigation property, as it is handled via UserFollow
                entity.Ignore(u => u.FollowedUsers);
            });

            // UserFollow configuration
            modelBuilder.Entity<UserFollow>(entity =>
            {
                entity.HasKey(uf => uf.Id);

                entity.HasOne<UserInfo>()
                    .WithMany()
                    .HasForeignKey(uf => uf.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<UserInfo>()
                    .WithMany()
                    .HasForeignKey(uf => uf.FollowedUserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }

}
