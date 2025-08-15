using StayTrackPro.API.DTOs;

namespace StayTrackPro.API.Services.Interfaces
{
    public interface IReservationService
    {
        Task<List<ReservationDto>> GetAllAsync(CancellationToken ct = default);
        Task<ReservationDto?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<ReservationDto> CreateAsync(ReservationDto dto, CancellationToken ct = default);
        Task<bool> UpdateAsync(int id, ReservationDto dto, CancellationToken ct = default);
        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}
