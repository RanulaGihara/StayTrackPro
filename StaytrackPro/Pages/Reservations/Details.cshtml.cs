using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Reservations;

public class DetailsModel : PageModel
{
    [BindProperty]
    public Reservation Reservation { get; set; }

   [BindProperty(SupportsGet = true)]
    public int? EditingRequestId { get; set; }


    public IActionResult OnGet(int id)
    {
        Reservation = AppMemoryContext.Reservations.FirstOrDefault(r => r.Id == id);
        if (Reservation == null)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }

    public IActionResult OnPost(
        int id,
        string? action,
        int? editRequestId,
        string? updatedRequestContent,
        string? newRequest,
        int? deleteRequestId)
    {
        Reservation = AppMemoryContext.Reservations.FirstOrDefault(r => r.Id == id);
        if (Reservation == null) return RedirectToPage("Index");

        if (action == "edit" && editRequestId.HasValue)
        {
            EditingRequestId = editRequestId;
            return Page();
        }

        if (action == "update" && editRequestId.HasValue && !string.IsNullOrWhiteSpace(updatedRequestContent))
        {
            var toUpdate = Reservation.SpecialRequests.FirstOrDefault(r => r.Id == editRequestId);
            if (toUpdate != null)
            {
                toUpdate.Content = updatedRequestContent.Trim();
            }
            return RedirectToPage("Details", new { id });
        }


        if (deleteRequestId.HasValue)
        {
            Reservation.SpecialRequests.RemoveAll(r => r.Id == deleteRequestId);
            return RedirectToPage("Details", new { id });
        }

        if (!string.IsNullOrWhiteSpace(newRequest))
        {
            var newId = Reservation.SpecialRequests.Any()
                ? Reservation.SpecialRequests.Max(r => r.Id) + 1
                : 1;

            Reservation.SpecialRequests.Add(new SpecialRequest
            {
                Id = newId,
                Content = newRequest.Trim()
            });
        }

        return RedirectToPage("Details", new { id });
    }
}
