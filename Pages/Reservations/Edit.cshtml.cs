using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Reservations;

public class EditModel : PageModel
{
    [BindProperty]
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

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var existing = AppMemoryContext.Reservations.FirstOrDefault(r => r.Id == Reservation.Id);
        if (existing != null)
        {
            existing.GuestFullName = Reservation.GuestFullName;
            existing.ArrivalDate = Reservation.ArrivalDate;
            existing.DepartureDate = Reservation.DepartureDate;
            existing.SuiteId = Reservation.SuiteId;
        }

        return RedirectToPage("Index");
    }
}
