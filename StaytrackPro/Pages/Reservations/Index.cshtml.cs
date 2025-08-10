using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Shared.Models;
using System.Net.Http.Json;

namespace StayTrackPro.Pages.Reservations
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<Reservation> Reservations { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("StayTrackProApi");

            var response = await client.GetAsync("reservations");
            if (response.IsSuccessStatusCode)
            {
                var reservations = await response.Content.ReadFromJsonAsync<List<Reservation>>();
                if (reservations != null)
                    Reservations = reservations;
            }
            else
            {
               
                Reservations = new List<Reservation>();
            }
        }
    }
}
