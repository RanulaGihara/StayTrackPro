using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Suites;

public class EditModel : PageModel
{
    [BindProperty]
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

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var existing = AppMemoryContext.Suites.FirstOrDefault(s => s.Id == Suite.Id);
        if (existing != null)
        {
            existing.SuiteName = Suite.SuiteName;
            existing.Type = Suite.Type;
        }

        return RedirectToPage("Index");
    }
}
