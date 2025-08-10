using System.ComponentModel.DataAnnotations;

namespace StayTrackPro.API.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "SuiteId is required.")]
        public int SuiteId { get; set; }

        [Required(ErrorMessage = "Guest name is required.")]
        [StringLength(100, ErrorMessage = "Guest name cannot exceed 100 characters.")]
        public string GuestName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Guest email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100)]
        public string GuestEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Check-in date is required.")]
        public DateTime CheckIn { get; set; }

        [Required(ErrorMessage = "Check-out date is required.")]
        public DateTime CheckOut { get; set; }

        [Required(ErrorMessage = "Number of guests is required.")]
        [Range(1, 20, ErrorMessage = "Number of guests must be between 1 and 20.")]
        public int NumberOfGuests { get; set; }

        public Suite? Suite { get; set; }
    }
}
