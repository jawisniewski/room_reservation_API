using RoomBooking.Application.DTOs.Room;
using RoomBooking.Domain.Entitis.Room;

namespace RoomBooking.Services.Interfaces.Repositories
{
    public interface IRoomQueryRepository
    {
        public Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime from, DateTime to, RoomFilters roomFilter);
    }
}
