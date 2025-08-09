using Microsoft.EntityFrameworkCore;

namespace StayTrackPro.API.Models
{
    public class StayTrackProDbContext : DbContext
    {
        public StayTrackProDbContext(DbContextOptions<StayTrackProDbContext> options) : base(options) { }

        public DbSet<Suite> Suites { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

    }
}
