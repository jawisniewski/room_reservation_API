using RoomBooking.Application.DTOs.Equipment;
using RoomBooking.Domain.ValueObjects;
using RoomsReservation.Domain.Entitis;
using RoomsReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.DTOs.Room
{
    public class BaseRoomRequest
    {
        public required string Name { get; set; }
        public int Capacity { get; set; }
        public int TableCount { get; set; }
        public RoomLayoutEnum Layout { get; set; }
        public RoomReservationLimitDto? ReservationLimit { get; set; }
        public List<EquipmentDto> Equipments { get; set; } = new List<EquipmentDto>();
    }
}
