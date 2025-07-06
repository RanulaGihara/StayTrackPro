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

    public IActionResult OnPost(int id, string? newRequest, int? deleteRequestId)
    {
        var reservation = AppMemoryContext.Reservations.FirstOrDefault(r => r.Id == id);
        if (reservation == null)
        {
            return RedirectToPage("Index");
        }

        if (!string.IsNullOrWhiteSpace(newRequest))
        {
            var newId = reservation.SpecialRequests.Any()
                ? reservation.SpecialRequests.Max(r => r.Id) + 1
                : 1;

            reservation.SpecialRequests.Add(new SpecialRequest
            {
                Id = newId,
                Content = newRequest.Trim()
            });
        }
        else if (deleteRequestId.HasValue)
        {
            var toRemove = reservation.SpecialRequests.FirstOrDefault(r => r.Id == deleteRequestId.Value);
            if (toRemove != null)
            {
                reservation.SpecialRequests.Remove(toRemove);
            }
        }

        return RedirectToPage("Details", new { id });
    }
}
