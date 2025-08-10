using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StayTrackPro.Shared.Models;
using System.Net.Http.Json;

namespace StayTrackPro.Pages.Suites
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(IHttpClientFactory httpClientFactory, ILogger<CreateModel> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [BindProperty]
        public Suite Suite { get; set; } = new Suite();

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var client = _httpClientFactory.CreateClient("StayTrackProApi");

                var response = await client.PostAsJsonAsync("suites", Suite);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Error creating suite. StatusCode: {StatusCode}, Response: {ErrorContent}", 
                                     response.StatusCode, errorContent);

                    ModelState.AddModelError(string.Empty, $"Error creating suite: {errorContent}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred while creating suite.");
                ModelState.AddModelError(string.Empty, $"An unexpected error occurred: {ex.Message}");
            }

            return Page();
        }
    }
}
