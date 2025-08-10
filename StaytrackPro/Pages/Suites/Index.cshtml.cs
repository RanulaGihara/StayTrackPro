using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Shared.Models;
using System.Net.Http.Json;

namespace StayTrackPro.Pages.Suites
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IndexModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public List<Suite> Suites { get; set; } = new();

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("StayTrackProApi");
            var response = await client.GetAsync("suites");

            if (response.IsSuccessStatusCode)
            {
                Suites = await response.Content.ReadFromJsonAsync<List<Suite>>() ?? new();
            }
        }
    }
}
