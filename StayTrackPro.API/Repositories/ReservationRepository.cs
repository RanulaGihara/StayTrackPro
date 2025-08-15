using Microsoft.EntityFrameworkCore;
using StayTrackPro.API.Models;
using StayTrackPro.API.Repositories.Interfaces;

namespace StayTrackPro.API.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly StayTrackProDbContext _context;

        public ReservationRepository(StayTrackProDbContext context)
        {
            _context = context;
        }

        public Task<List<Reservation>> GetAllAsync(CancellationToken ct = default) =>
            _context.Reservations.AsNoTracking().ToListAsync(ct);

        public Task<Reservation?> GetByIdAsync(int id, CancellationToken ct = default) =>
            _context.Reservations.FindAsync(new object?[] { id }, ct).AsTask();

        public async Task AddAsync(Reservation reservation, CancellationToken ct = default) =>
            await _context.Reservations.AddAsync(reservation, ct);

        public void Update(Reservation reservation) =>
            _context.Reservations.Update(reservation);

        public void Remove(Reservation reservation) =>
            _context.Reservations.Remove(reservation);

        public Task<bool> ExistsAsync(int id, CancellationToken ct = default) =>
            _context.Reservations.AnyAsync(r => r.Id == id, ct);
    }
}
