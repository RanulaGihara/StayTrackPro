namespace StayTrackPro.API.Models
{
    public class Suite
    {
        public int Id { get; set; }
        public string SuiteType { get; set; } = string.Empty;
        public decimal PricePerNight { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }
}
