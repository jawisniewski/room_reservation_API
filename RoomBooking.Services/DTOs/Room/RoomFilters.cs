using RoomBooking.Application.DTOs.Equipment;
using RoomBooking.Domain.ValueObjects;
using RoomsReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.DTOs.Room
{
    public class RoomFilters
    {
        public string? Name { get; private set; }
        public int? Capacity { get; private set; }
        public int? TableCount { get; private set; }
        public RoomLayoutEnum? Layout { get; private set; }
        public IEnumerable<EquipmentDto>? Equipments { get; private set; }
    }
}
