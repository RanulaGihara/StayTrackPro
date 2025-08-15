using Microsoft.EntityFrameworkCore;
using StayTrackPro.API.DTOs;
using StayTrackPro.API.Mappings;
using StayTrackPro.API.Repositories.Interfaces;

namespace StayTrackPro.API.Services
{
    public class ReservationService : Interfaces.IReservationService
    {
        private readonly IReservationRepository _repo;
        private readonly IUnitOfWork _uow;

        public ReservationService(IReservationRepository repo, IUnitOfWork uow)
        {
            _repo = repo;
            _uow = uow;
        }

            public async Task<List<ReservationDto>> GetAllAsync(CancellationToken ct = default)
        {
            var entities = await _repo.GetAllAsync(ct);
            return entities.Select(e => e.ToDto()).ToList();
        }

        public async Task<ReservationDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            return entity?.ToDto();
        }

        public async Task<ReservationDto> CreateAsync(ReservationDto dto, CancellationToken ct = default)
        {
            var entity = dto.ToEntity();
            await _repo.AddAsync(entity, ct);
            await _uow.SaveChangesAsync(ct);
            return entity.ToDto();
        }

        public async Task<bool> UpdateAsync(int id, ReservationDto dto, CancellationToken ct = default)
        {
            if (id != dto.Id) return false;

            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity == null) return false;

            entity.UpdateFromDto(dto);
            _repo.Update(entity);

            try
            {
                await _uow.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var entity = await _repo.GetByIdAsync(id, ct);
            if (entity == null) return false;

            _repo.Remove(entity);
            await _uow.SaveChangesAsync(ct);
            return true;
        }
    }
}
