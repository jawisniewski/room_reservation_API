using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsReservation.Domain.Entitis
{
    public class User
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required string Email { get; set; }
        public required string Password { get; set; }
        public List<Reservation>? Reservations { get; set; } = [];
    }
}
