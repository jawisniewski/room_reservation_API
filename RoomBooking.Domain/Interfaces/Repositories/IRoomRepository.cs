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
        public Task<Guid> CreateAsync(Room room);
        public Task<bool> UpdateAsync(Room room);
        public Task<bool> DeleteAsync(Guid roomId); 
        public Task<bool> IsAvailableAsync(Guid room, DateTime from, DateTime to, Guid? excludedReservationId = null);
        public Task<Room?> GetByIdAsync(Guid id);
        public Task<Room?> GetByNameAsync(string name);
    }
}
