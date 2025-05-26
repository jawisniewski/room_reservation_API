using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Application.DTOs.Reservation
{
    public class BaseReservationRequest
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid RoomId { get; set; }
    }
}
