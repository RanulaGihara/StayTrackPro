namespace StayTrackPro.Shared.Models
{
    public class Suite
    {
        public int Id { get; set; }
         public string SuiteName { get; set; }
        public string Type  { get; set; } = string.Empty;

        public decimal PricePerNight { get; set; }
    }
}
