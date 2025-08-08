using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Suites;

public class CreateModel : PageModel
{
    [BindProperty]
    public Suite Suite { get; set; }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Suite.Id = AppMemoryContext.Suites.Count > 0
            ? AppMemoryContext.Suites.Max(s => s.Id) + 1
            : 1;

        AppMemoryContext.Suites.Add(Suite);
        return RedirectToPage("Index");
    }
}
