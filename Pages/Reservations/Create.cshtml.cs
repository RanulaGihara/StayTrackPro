using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Reservations;

public class CreateModel : PageModel
{
    [BindProperty]
    public Reservation Reservation { get; set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Reservation.Id = AppMemoryContext.Reservations.Count > 0
            ? AppMemoryContext.Reservations.Max(r => r.Id) + 1
            : 1;

        AppMemoryContext.Reservations.Add(Reservation);
        return RedirectToPage("Index");
    }
}
