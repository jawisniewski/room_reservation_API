using RoomBooking.Domain.Entitis.Room;
using RoomBooking.Services.DTOs;
using RoomBooking.Services.Interfaces.Repositories;
using RoomsReservation.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository, IRoomQueryRepository
    {
        public Task<int> Create(Room room)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(Room room)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetAvailableRooms(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime from, DateTime to, RoomFilters roomFilter)
        {
            throw new NotImplementedException();
        }

        public Task<Room> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsAvailableAsync(Guid room, DateTime from, DateTime to, Guid? excludedReservationId = null)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Room room)
        {
            throw new NotImplementedException();
        }
    }
}
