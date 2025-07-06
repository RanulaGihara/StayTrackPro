using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Reservations
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Reservation Reservation { get; set; }

        public List<Suite> AvailableSuites { get; set; }

        public IActionResult OnGet(int id)
        {
            Reservation = AppMemoryContext.Reservations.FirstOrDefault(r => r.Id == id);
            if (Reservation == null)
            {
                return RedirectToPage("Index");
            }

            AvailableSuites = AppMemoryContext.Suites;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                AvailableSuites = AppMemoryContext.Suites;
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
}
