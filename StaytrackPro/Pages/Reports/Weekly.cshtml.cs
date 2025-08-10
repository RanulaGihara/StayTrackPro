using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Shared.Models;

namespace StayTrackPro.Pages.Reports
{
    public class WeeklyModel : PageModel
    {
        public Dictionary<DayOfWeek, List<Reservation>> WeeklyReservations { get; set; }

        public void OnGet()
        {
            WeeklyReservations = AppMemoryContext.Reservations
                .GroupBy(r => r.CheckIn.DayOfWeek)
                .OrderBy(g => (int)g.Key)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public string GetSuiteName(int suiteId)
        {
            return AppMemoryContext.Suites.FirstOrDefault(s => s.Id == suiteId)?.Type ?? "Unknown";
        }
    }
}
