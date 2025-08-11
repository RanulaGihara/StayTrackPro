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
        public Reservation Reservation { get; set; } = new Reservation();

        public List<Suite> AvailableSuites { get; set; } = new();

        // GET: Load suites from API
        public async Task OnGetAsync()
        {
            await LoadSuitesAsync();
        }

        // POST: Create reservation in API
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadSuitesAsync(); // reload suites for dropdown
                return Page();
            }

            var client = _clientFactory.CreateClient("StayTrackProApi");

            try
            {
                var postResponse = await client.PostAsJsonAsync("reservations", Reservation);

                if (postResponse.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    ModelState.AddModelError("", $"Error creating reservation: {postResponse.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"API call failed: {ex.Message}");
            }

            await LoadSuitesAsync();
            return Page();
        }

        private async Task LoadSuitesAsync()
        {
            var client = _clientFactory.CreateClient("StayTrackProApi");

            try
            {
                var suites = await client.GetFromJsonAsync<List<Suite>>("suites");
                if (suites != null && suites.Any())
                {
                    AvailableSuites = suites;
                }
                else
                {
                    // Add placeholder if no suites found
                    AvailableSuites = new List<Suite>
                    {
                        new Suite { Id = 0, SuiteName = "No suites available" }
                    };
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error loading suites: {ex.Message}");
                AvailableSuites = new List<Suite>(); // Prevent null ref in Razor
            }
        }
    }
}
