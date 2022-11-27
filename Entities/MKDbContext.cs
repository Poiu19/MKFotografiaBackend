using Microsoft.EntityFrameworkCore;

namespace MKFotografiaBackend.Entities
{
    public class MKDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public MKDbContext(DbContextOptions<MKDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .Property(p => p.Id)
                .IsRequired();
        }
    }
}
