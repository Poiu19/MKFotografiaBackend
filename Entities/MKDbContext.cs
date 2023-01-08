using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace MKFotografiaBackend.Entities
{
    public class MKDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<GalleryPhoto> GalleryPhotos { get; set; }
        public DbSet<SliderPhoto> SliderPhotos { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public MKDbContext(DbContextOptions<MKDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            /* USER */
            modelBuilder.Entity<User>()
                .Property(p => p.Id)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.LastName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(320);
            modelBuilder.Entity<User>()
                .Property(p => p.PasswordHash)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(p => p.LastLogin)
                .HasDefaultValue(null);
            modelBuilder.Entity<User>()
                .Property(p => p.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<User>()
                .HasOne(p => p.Role)
                .WithMany()
                .HasForeignKey(p => p.RoleId)
                .OnDelete(DeleteBehavior.NoAction);

            /* ROLE */
            modelBuilder.Entity<Role>()
                .Property(p => p.Id)
                .IsRequired();
            modelBuilder.Entity<Role>()
                .Property(p => p.Name)
                .IsRequired();

            /* POST */
            modelBuilder.Entity<Post>()
                .Property(p => p.Id)
                .IsRequired();
            modelBuilder.Entity<Post>()
                .HasOne(p => p.User)
                .WithMany()
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            /* GALLERY */
            modelBuilder.Entity<Gallery>()
                .Property(p => p.Id)
                .IsRequired();
            modelBuilder.Entity<Gallery>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Gallery>()
                .Property(p => p.DisplayName)
                .IsRequired()
                .HasMaxLength(100);

            /* GALLERYPHOTO */
            modelBuilder.Entity<GalleryPhoto>()
                .Property(p => p.Id)
                .IsRequired();
            modelBuilder.Entity<GalleryPhoto>()
                .Property(p => p.AlternativeText)
                .IsRequired();
            modelBuilder.Entity<GalleryPhoto>()
                .HasOne(p => p.Gallery)
                .WithMany(p => p.Photos)
                .HasForeignKey(p => p.GalleryId)
                .OnDelete(DeleteBehavior.Cascade);

            /* SLIDERPHOTO */
            modelBuilder.Entity<SliderPhoto>()
                .Property(p => p.Id)
                .IsRequired();
            modelBuilder.Entity<SliderPhoto>()
                .Property(p => p.Title)
                .IsRequired();
            modelBuilder.Entity<SliderPhoto>()
                .Property(p => p.Subtitle)
                .IsRequired();
            modelBuilder.Entity<SliderPhoto>()
                .Property(p => p.AlternativeText)
                .IsRequired();
            modelBuilder.Entity<SliderPhoto>()
                .Property(p => p.Order)
                .HasDefaultValue(0);

            /* OFFER */
            modelBuilder.Entity<Offer>()
                .Property(p => p.Id)
                .IsRequired();
            modelBuilder.Entity<Offer>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Offer>()
                .Property(p => p.RelativeURL)
                .IsRequired();
            modelBuilder.Entity<Offer>()
                .Property(p => p.Price)
                .IsRequired();
            modelBuilder.Entity<Offer>()
                .Property(p => p.BasicFeatures)
                .HasConversion(
                    p => JsonConvert.SerializeObject(p),
                    p => JsonConvert.DeserializeObject<List<string>>(p)
                );
            modelBuilder.Entity<Offer>()
                .Property(p => p.AdditionalFeatures)
                .HasConversion(
                    p => JsonConvert.SerializeObject(p),
                    p => JsonConvert.DeserializeObject<List<string>>(p)
                );
            modelBuilder.Entity<Offer>()
                .Property(p => p.TeaserDesktop)
                .HasConversion(
                    p => JsonConvert.SerializeObject(p),
                    p => JsonConvert.DeserializeObject<List<string>>(p)
                );
            modelBuilder.Entity<Offer>()
                .HasOne(p => p.ConnectedOffer)
                .WithMany()
                .HasForeignKey(p => p.ConnectedOfferId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Offer>()
                .HasOne(p => p.ConnectedGallery)
                .WithMany()
                .HasForeignKey(p => p.ConnectedGalleryId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Offer>()
                .Property(p => p.ConnectedGalleryId)
                .HasDefaultValue(null);
        }
    }
}
