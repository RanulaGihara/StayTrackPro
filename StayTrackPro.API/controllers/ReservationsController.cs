using Microsoft.AspNetCore.Mvc;
using StayTrackPro.API.DTOs;
using StayTrackPro.API.Services.Interfaces;

namespace StayTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _service;

        public ReservationsController(IReservationService service)
        {
            _service = service;
        }

        // GET: api/reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations(CancellationToken ct)
        {
            var reservations = await _service.GetAllAsync(ct);
            return Ok(reservations);
        }

        // GET: api/reservations/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            if (dto == null) return NotFound();
            return Ok(dto);
        }

        // POST: api/reservations
        [HttpPost]
        public async Task<ActionResult> CreateReservation([FromBody] ReservationDto dto, CancellationToken ct)
        {
            var created = await _service.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetReservation), new { id = created.Id }, created);
        }

        // PUT: api/reservations/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateReservation(int id, [FromBody] ReservationDto dto, CancellationToken ct)
        {
            if (!await _service.UpdateAsync(id, dto, ct))
                return NotFound();

            return NoContent();
        }

        // DELETE: api/reservations/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteReservation(int id, CancellationToken ct)
        {
            if (!await _service.DeleteAsync(id, ct))
                return NotFound();

            return NoContent();
        }
    }
}
