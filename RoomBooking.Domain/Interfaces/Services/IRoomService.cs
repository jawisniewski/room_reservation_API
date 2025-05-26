using RoomBooking.Domain.ValueObjects;
using RoomsReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Interfaces.Services
{
    public interface IRoomService
    {
        public Task<Guid> CreateAsync(string name, int capacity, int tableCount, RoomLayoutEnum roomLayout, List<Equipment> equipments, RoomReservationLimit reservationLimit);
        public Task<bool> UpdateAsync(Guid roomId, string name, int capacity, int tableCount, RoomLayoutEnum roomLayout, List<Equipment> equipments, RoomReservationLimit reservationLimit);
        public Task<bool> DeleteAsync(Guid roomId);
    }
}
