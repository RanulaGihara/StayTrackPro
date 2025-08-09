using Microsoft.AspNetCore.Mvc;
using StayTrackPro.Shared.Models;

namespace StayTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private static List<Reservation> reservations = new();

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetAll()
        {
            return Ok(reservations);
        }

        [HttpPost]
        public ActionResult<Reservation> Create([FromBody] Reservation reservation)
        {
            reservation.Id = reservations.Count + 1;
            reservations.Add(reservation);
            return CreatedAtAction(nameof(GetAll), new { id = reservation.Id }, reservation);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var res = reservations.FirstOrDefault(r => r.Id == id);
            if (res == null) return NotFound();
            reservations.Remove(res);
            return NoContent();
        }
    }
}
