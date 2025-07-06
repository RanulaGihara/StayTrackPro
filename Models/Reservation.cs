namespace StayTrackPro.Models;

public class Reservation
{
    public int Id { get; set; }
    public string GuestFullName { get; set; }
    public int SuiteId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public List<SpecialRequest> SpecialRequests { get; set; } = new();
}
