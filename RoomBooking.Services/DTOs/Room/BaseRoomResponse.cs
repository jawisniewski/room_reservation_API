using RoomBooking.Application.DTOs.Equipment;
using RoomsReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.DTOs.Room
{
    public class RoomResponse
    {
        public Guid Id { get;private set; }
        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public int TableCount { get; private set; }
        public RoomLayoutEnum Layout { get; private set; }
        public RoomReservationLimitDto? ReservationLimit { get; private set; }
        public List<EquipmentDto> Equipments { get; private set; } = new List<EquipmentDto>();
    }
}
