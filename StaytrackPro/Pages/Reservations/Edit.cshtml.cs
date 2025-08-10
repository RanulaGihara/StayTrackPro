using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StayTrackPro.Shared.Models;
using System.Net.Http.Json;

namespace StayTrackPro.Pages.Reservations
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public EditModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public List<Suite> AvailableSuites { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient("StayTrackProApi");

            // Get reservation by ID
            Reservation = await client.GetFromJsonAsync<Reservation>($"reservations/{id}");
            if (Reservation == null)
            {
                return RedirectToPage("Index");
            }

            // Get suites list for dropdown
            var suitesResponse = await client.GetFromJsonAsync<List<Suite>>("suites");
            if (suitesResponse != null)
            {
                AvailableSuites = suitesResponse;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var client = _clientFactory.CreateClient("StayTrackProApi");
                AvailableSuites = await client.GetFromJsonAsync<List<Suite>>("suites") ?? new();
                return Page();
            }

            var http = _clientFactory.CreateClient("StayTrackProApi");
            var response = await http.PutAsJsonAsync($"reservations/{Reservation.Id}", Reservation);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Failed to update reservation.");
                AvailableSuites = await http.GetFromJsonAsync<List<Suite>>("suites") ?? new();
                return Page();
            }

            return RedirectToPage("Index");
        }
    }
}
