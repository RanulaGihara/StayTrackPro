using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StayTrackPro.API.DTOs;
using StayTrackPro.API.Models;


namespace StayTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly StayTrackProDbContext _context;

        public ReservationsController(StayTrackProDbContext context)
        {
            _context = context;
        }

        // GET: api/reservations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetReservations()
        {
            var reservations = await _context.Reservations
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    SuiteId = r.SuiteId,
                    GuestName = r.GuestName,
                    GuestEmail = r.GuestEmail,
                    CheckIn = r.CheckIn,
                    CheckOut = r.CheckOut,
                    NumberOfGuests = r.NumberOfGuests
                }).ToListAsync();

            return Ok(reservations);
        }

        // GET: api/reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetReservation(int id)
        {
            var r = await _context.Reservations.FindAsync(id);
            if (r == null) return NotFound();

            return Ok(new ReservationDto
            {
                Id = r.Id,
                SuiteId = r.SuiteId,
                GuestName = r.GuestName,
                GuestEmail = r.GuestEmail,
                CheckIn = r.CheckIn,
                CheckOut = r.CheckOut,
                NumberOfGuests = r.NumberOfGuests
            });
        }

        // POST: api/reservations
        [HttpPost]
        public async Task<ActionResult> CreateReservation(ReservationDto dto)
        {
            var reservation = new Reservation
            {
                SuiteId = dto.SuiteId,
                GuestName = dto.GuestName,
                GuestEmail = dto.GuestEmail,
                CheckIn = dto.CheckIn,
                CheckOut = dto.CheckOut,
                NumberOfGuests = dto.NumberOfGuests
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            dto.Id = reservation.Id;
            return CreatedAtAction(nameof(GetReservation), new { id = dto.Id }, dto);
        }

        // PUT: api/reservations/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateReservation(int id, ReservationDto dto)
        {
            if (id != dto.Id) return BadRequest();

            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            reservation.SuiteId = dto.SuiteId;
            reservation.GuestName = dto.GuestName;
            reservation.GuestEmail = dto.GuestEmail;
            reservation.CheckIn = dto.CheckIn;
            reservation.CheckOut = dto.CheckOut;
            reservation.NumberOfGuests = dto.NumberOfGuests;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReservation(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null) return NotFound();

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
