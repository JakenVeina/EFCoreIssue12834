using Microsoft.EntityFrameworkCore;

namespace EFCoreIssue12834
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options) { }

        public virtual DbSet<MyPrimaryEntity> PrimaryEntities { get; set; }

        public virtual DbSet<MySecondaryEntity> SecondaryEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<MyPrimaryEntity>()
                .Property(x => x.Type)
                .HasConversion<string>();

            modelBuilder
                .Entity<MySecondaryEntity>()
                .Property(x => x.Type)
                .HasConversion<string>();
        }
    }
}
