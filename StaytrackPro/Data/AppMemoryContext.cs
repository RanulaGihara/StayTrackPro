using StayTrackPro.Models;

namespace StayTrackPro.Data;

public static class AppMemoryContext
{
    public static List<Reservation> Reservations { get; set; } = new();
    public static List<Suite> Suites { get; set; } = new()
    {
        new Suite { Id = 1, SuiteName = "Ocean View", Type = "Deluxe" },
        new Suite { Id = 2, SuiteName = "Garden Bliss", Type = "Double" },
        new Suite { Id = 3, SuiteName = "City Classic", Type = "Single" }
    };
}
