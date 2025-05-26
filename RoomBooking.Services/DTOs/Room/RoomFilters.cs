using RoomBooking.Application.DTOs.Equipment;
using RoomsReservation.Domain.Enums;

namespace RoomBooking.Application.DTOs.Room
{
    public class RoomFilters
    {
        public string? Name { get; private set; }
        public int? Capacity { get; private set; }
        public int? TableCount { get; private set; }
        public RoomLayoutEnum? Layout { get; private set; }
        public IEnumerable<EquipmentFilterRequest>? Equipments { get; private set; }
    }
}
