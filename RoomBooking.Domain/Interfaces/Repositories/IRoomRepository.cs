using RoomBooking.Domain.Entitis.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsReservation.Domain.Interfaces.Repositories
{
    public interface IRoomRepository
    {
        public Task<int> Create(Room room);
        public Task<int> Update(Room room);
        public Task<int> Delete(Room room); 
        public Task<bool> IsAvailableAsync(Guid room, DateTime from, DateTime to, Guid? excludedReservationId = null);
        public Task<Room> GetByIdAsync(Guid id);
        public Task<IEnumerable<Room>> GetAvailableRooms(DateTime from, DateTime to);
    }
}
