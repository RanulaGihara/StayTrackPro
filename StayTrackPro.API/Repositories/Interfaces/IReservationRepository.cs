using StayTrackPro.API.Models;

namespace StayTrackPro.API.Repositories.Interfaces
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllAsync(CancellationToken ct = default);
        Task<Reservation?> GetByIdAsync(int id, CancellationToken ct = default);
        Task AddAsync(Reservation reservation, CancellationToken ct = default);
        void Update(Reservation reservation);
        void Remove(Reservation reservation);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);
    }
}
