namespace StayTrackPro.API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public int SuiteId { get; set; }
        public string GuestName { get; set; } = string.Empty;
        public string GuestEmail { get; set; } = string.Empty;
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumberOfGuests { get; set; }

        public Suite? Suite { get; set; }
    }
}
