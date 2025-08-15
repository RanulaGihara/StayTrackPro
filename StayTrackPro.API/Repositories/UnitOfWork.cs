using StayTrackPro.API.Models;
using StayTrackPro.API.Repositories.Interfaces;

namespace StayTrackPro.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StayTrackProDbContext _context;

        public UnitOfWork(StayTrackProDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return _context.SaveChangesAsync(ct);
        }
    }
}
