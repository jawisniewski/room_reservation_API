using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Interfaces.Services
{
    public interface IRoomReservationService
    {
        public Task<Guid> ReserveRoom(Guid roomId, DateTime from, DateTime to, Guid userId);
        public Task<bool> UpdateReservation(Guid reservationId, Guid roomId, DateTime from, DateTime to, Guid userId);
        public Task<bool> CancelReservation(Guid reservationId, Guid userId);
    }
}
