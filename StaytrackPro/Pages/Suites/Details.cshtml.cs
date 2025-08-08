using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Suites;

public class DetailsModel : PageModel
{
    public Suite Suite { get; set; }

    public IActionResult OnGet(int id)
    {
        Suite = AppMemoryContext.Suites.FirstOrDefault(s => s.Id == id);

        if (Suite == null)
        {
            return RedirectToPage("Index");
        }

        return Page();
    }
}
