using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Shared.Models;
using System.Net.Http.Json;

namespace StayTrackPro.Pages.Suites
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Suite Suite { get; set; }

        // GET: Load suite details from API
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient("StayTrackProApi");
            Suite = await client.GetFromJsonAsync<Suite>($"suites/{id}");

            if (Suite == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        // POST: Send delete request to API
        public async Task<IActionResult> OnPostAsync()
        {
            var client = _httpClientFactory.CreateClient("StayTrackProApi");
            var response = await client.DeleteAsync($"suites/{Suite.Id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error deleting suite.");
            return Page();
        }
    }
}
