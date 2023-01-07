using Microsoft.EntityFrameworkCore;
using MKFotografiaBackend.Entities;

namespace MKFotografiaBackend.Components
{
    public class MKSeeder
    {
        private readonly MKDbContext _dbContext;
        public MKSeeder(MKDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    _dbContext.Database.Migrate();
                }
                if (!_dbContext.Roles.Any())
                {
                    FillRoles();
                }
                if (!_dbContext.Users.Any())
                {
                    FillUsers();
                }
                if (!_dbContext.SliderPhotos.Any())
                {
                    FillSliderPhotos();
                }
            }
        }
        public void FillSliderPhotos()
        {
            SliderPhoto[] photos = new SliderPhoto[]
            {
                new SliderPhoto()
                {
                    Title = "Naturalna Fotografia Ślubna",
                    Subtitle = "Profesjonalna oprawa fotograficzna najważniejszego dnia w Waszym życiu.",
                    AlternativeText = "Młoda para całująca się w otoczeniu drzew.",
                    Path = "slider-photo/1.webp"
                },
                new SliderPhoto() {
                    Title = "Profesjonalny Reportaż Ślubny",
                    Subtitle = "Wyjątkowe zdjęcia z najważniejszych chwil w Waszym życiu.",
                    AlternativeText = "Pan młody trzymający pannę młodą w wodzie.",
                    Path = "slider-photo/2.webp"
                },
                new SliderPhoto() {
                    Title = "Fotografia Weselna",
                    Subtitle = "Szczere i autentyczne zdjęcia będą pamiątką na lata.",
                    AlternativeText = "Tańcząca para młoda.",
                    Path = "slider-photo/3.webp"
                }
            };
            _dbContext.SliderPhotos.AddRange(photos);
            _dbContext.SaveChanges();
        }
        public void FillRoles()
        {
            Role[] roles = new Role[]
            {
                new Role()
                {
                    Name = "Użytkownik"
                },
                new Role()
                {
                    Name = "Moderator"
                },
                new Role()
                {
                    Name = "Administrator"
                },
                new Role()
                {
                    Name = "Developer"
                }
            };
            _dbContext.Roles.AddRange(roles);
            _dbContext.SaveChanges();
        }
        public void FillUsers()
        {
            var admin = new User()
            {
                Active = true,
                Email = "admin@admin.pl",
                Name = "Admin",
                LastName = "Admin",
                RoleId = 4,
                PasswordHash = ""
            };
            _dbContext.Users.Add(admin);
            _dbContext.SaveChanges();
        }
    }
}
