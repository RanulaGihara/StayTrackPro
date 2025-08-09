using Microsoft.AspNetCore.Mvc;
using StayTrackPro.Shared.Models;

namespace StayTrackPro.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuitesController : ControllerBase
    {
        private static List<Suite> suites = new()
        {
            new Suite { Id = 1, SuiteName = "Ocean View", Type = "Deluxe" },
            new Suite { Id = 2, SuiteName = "Mountain Retreat", Type = "Standard" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Suite>> GetAll()
        {
            return Ok(suites);
        }
    }
}
