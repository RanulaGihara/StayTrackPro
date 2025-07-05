using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Reservations;

public class DetailsModel : PageModel
{
    public Reservation Reservation { get; set; }

    public IActionResult OnGet(int id)
    {
        Reservation = AppMemoryContext.Reservations.FirstOrDefault(r => r.Id == id);

        if (Reservation == null)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }
}
