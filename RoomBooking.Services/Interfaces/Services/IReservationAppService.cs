using RoomBooking.Application.Common;
using RoomBooking.Application.DTOs.Reservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.Interfaces.Services
{
    public interface IReservationAppService
    {
        public Task<Result<Guid>> CreateAsync(BaseReservationRequest reservation, Guid userId);
        public Task<Result> CancelAsync(Guid reservationId, Guid userId);
        public Task<Result> UpdateAsync(UpdateReservationRequest updateReservation, Guid userId);
    }
}
