using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Data;
using StayTrackPro.Models;

namespace StayTrackPro.Pages.Suites;

public class DeleteModel : PageModel
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
        var existing = AppMemoryContext.Suites.FirstOrDefault(s => s.Id == Suite.Id);
        if (existing != null)
        {
            AppMemoryContext.Suites.Remove(existing);
        }

        return RedirectToPage("Index");
    }
}
