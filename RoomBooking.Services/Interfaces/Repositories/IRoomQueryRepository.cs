using RoomBooking.Domain.Entitis.Room;
using RoomBooking.Services.DTOs;

namespace RoomBooking.Services.Interfaces.Repositories
{
    public interface IRoomQueryRepository
    {
        public Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime from, DateTime to, RoomFilters roomFilter);
    }
}
