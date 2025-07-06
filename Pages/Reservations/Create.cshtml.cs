using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Reservations;

public class CreateModel : PageModel
{
    [BindProperty]
    public Reservation Reservation { get; set; }

    public List<Suite> AvailableSuites { get; set; }

    public void OnGet()
    {
        AvailableSuites = AppMemoryContext.Suites;
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            AvailableSuites = AppMemoryContext.Suites; // re-bind dropdown on validation error
            return Page();
        }

        Reservation.Id = AppMemoryContext.Reservations.Count > 0
            ? AppMemoryContext.Reservations.Max(r => r.Id) + 1
            : 1;

        AppMemoryContext.Reservations.Add(Reservation);
        return RedirectToPage("Index");
    }
}
