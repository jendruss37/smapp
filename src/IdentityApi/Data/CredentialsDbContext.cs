using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Data
{
    public class CredentialsDbContext : DbContext
    {
        public CredentialsDbContext(DbContextOptions<CredentialsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Credentials> UserCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Credentials>(entity =>
            {
                entity.HasKey(u => u.Id);

                entity.Property(u => u.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
