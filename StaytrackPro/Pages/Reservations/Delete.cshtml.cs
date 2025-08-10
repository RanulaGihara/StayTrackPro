using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Shared.Models;
using System.Net.Http.Json;

namespace StayTrackPro.Pages.Reservations
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public DeleteModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty]
        public Reservation Reservation { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var client = _clientFactory.CreateClient("StayTrackProApi");
            var response = await client.GetAsync($"reservations/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            Reservation = await response.Content.ReadFromJsonAsync<Reservation>();

            if (Reservation == null)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var client = _clientFactory.CreateClient("StayTrackProApi");
            var response = await client.DeleteAsync($"reservations/{Reservation.Id}");

            return RedirectToPage("Index");
        }
    }
}
