using Microsoft.EntityFrameworkCore;
using PostApi.Models;

namespace PostApi.Data
{


    public class PostDbContext : DbContext
    {
        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Posts table
            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Posts");

                entity.HasKey(p => p.Id);

                entity.Property(p => p.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(p => p.Content)
                      .HasColumnType("NVARCHAR(MAX)")
                      .IsRequired();

                entity.Property(p => p.UserId)
                      .IsRequired();
                


                entity.Property(p => p.CreatedAt)
                      .HasDefaultValueSql("GETDATE()")
                      .IsRequired();


            });

            

            base.OnModelCreating(modelBuilder);
        }
    }

}
