using Microsoft.EntityFrameworkCore;
using MKFotografiaBackend.Entities;

namespace MKFotografiaBackend
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
            }
        }
    }
}
