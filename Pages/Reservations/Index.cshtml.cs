using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Reservations;

public class IndexModel : PageModel
{
    public List<Reservation> Reservations { get; set; }

    public void OnGet()
    {
        Reservations = AppMemoryContext.Reservations;
    }
}
