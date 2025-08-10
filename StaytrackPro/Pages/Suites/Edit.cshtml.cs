using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Shared.Models;
using System.Net.Http.Json;

namespace StayTrackPro.Pages.Suites
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public Suite Suite { get; set; }

        // GET: Load suite data from API
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

        // POST: Send update request to API
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var client = _httpClientFactory.CreateClient("StayTrackProApi");
            var response = await client.PutAsJsonAsync($"suites/{Suite.Id}", Suite);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "Error updating suite.");
            return Page();
        }
    }
}
