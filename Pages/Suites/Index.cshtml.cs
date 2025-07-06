using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Suites;

public class IndexModel : PageModel
{
    public List<Suite> Suites { get; set; }

    public void OnGet()
    {
        Suites = AppMemoryContext.Suites;
    }
}
