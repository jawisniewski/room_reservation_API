using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.DTOs.Reservation
{
    public class UpdateReservationRequest : BaseReservationRequest
    {
        public Guid Id { get; set; }

    }
}
