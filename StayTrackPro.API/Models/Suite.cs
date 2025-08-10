using System.ComponentModel.DataAnnotations;

namespace StayTrackPro.API.Models
{
    public class Suite
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Suite name is required.")]
        [StringLength(100, ErrorMessage = "Suite name cannot exceed 100 characters.")]
        public string SuiteName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Type is required.")]
        [StringLength(50, ErrorMessage = "Type cannot exceed 50 characters.")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price per night is required.")]
        [Range(0, 999999.99, ErrorMessage = "Price must be a positive value.")]
        public decimal PricePerNight { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }
}
