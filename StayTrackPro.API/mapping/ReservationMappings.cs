using StayTrackPro.API.DTOs;
using StayTrackPro.API.Models;

namespace StayTrackPro.API.Mappings
{
    public static class ReservationMappings
    {
        public static ReservationDto ToDto(this Reservation r) => new()
        {
            Id = r.Id,
            SuiteId = r.SuiteId,
            GuestName = r.GuestName,
            GuestEmail = r.GuestEmail,
            CheckIn = r.CheckIn,
            CheckOut = r.CheckOut,
            NumberOfGuests = r.NumberOfGuests
        };

        public static Reservation ToEntity(this ReservationDto dto) => new()
        {
            Id = dto.Id,
            SuiteId = dto.SuiteId,
            GuestName = dto.GuestName,
            GuestEmail = dto.GuestEmail,
            CheckIn = dto.CheckIn,
            CheckOut = dto.CheckOut,
            NumberOfGuests = dto.NumberOfGuests
        };

        public static void UpdateFromDto(this Reservation entity, ReservationDto dto)
        {
            entity.SuiteId = dto.SuiteId;
            entity.GuestName = dto.GuestName;
            entity.GuestEmail = dto.GuestEmail;
            entity.CheckIn = dto.CheckIn;
            entity.CheckOut = dto.CheckOut;
            entity.NumberOfGuests = dto.NumberOfGuests;
        }
    }
}
