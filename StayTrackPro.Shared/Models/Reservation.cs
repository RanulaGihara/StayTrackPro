namespace StayTrackPro.Shared.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string GuestName { get; set; }
        public int SuiteId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public List<SpecialRequest> SpecialRequests { get; set; } = new();
    }
}
