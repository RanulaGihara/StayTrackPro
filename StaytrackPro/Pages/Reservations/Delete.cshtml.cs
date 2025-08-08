using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Reservations;

public class DeleteModel : PageModel
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
        var existing = AppMemoryContext.Reservations.FirstOrDefault(r => r.Id == Reservation.Id);
        if (existing != null)
        {
            AppMemoryContext.Reservations.Remove(existing);
        }

        return RedirectToPage("Index");
    }
}
