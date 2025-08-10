using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Shared.Models;
using System.Net.Http.Json;

namespace StayTrackPro.Pages.Reservations
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public CreateModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public List<Suite> AvailableSuites { get; set; } = new();

        // GET: Load suites from API
        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("StayTrackProApi");

            var suitesResponse = await client.GetAsync("suites");
            if (suitesResponse.IsSuccessStatusCode)
            {
                var suites = await suitesResponse.Content.ReadFromJsonAsync<List<Suite>>();
                if (suites != null)
                    AvailableSuites = suites;
            }
        }

        // POST: Create reservation in API
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // reload suites for dropdown
                return Page();
            }

            var client = _clientFactory.CreateClient("StayTrackProApi");
            var postResponse = await client.PostAsJsonAsync("reservations", Reservation);

            if (postResponse.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError("", "Error creating reservation. Please try again.");
            await OnGetAsync();
            return Page();
        }
    }
}
