using RoomBooking.Domain.Entitis.Room;
using RoomBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsReservation.Domain.Entitis
{
    public class Reservation
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public Guid RoomId { get; private set; }
        public Guid UserId { get; private set; }
        public virtual Room? Room { get; private set; }
        public virtual User? User { get; private set; }

        private Reservation() { }
        public Reservation(DateTime from, DateTime to, Guid roomId, Guid userId)
        {
            Validate(from, to, roomId, userId);

            From = from;
            To = to;
            RoomId = roomId;
            UserId = userId;
        }
        private void Validate(DateTime from, DateTime to, Guid roomId, Guid userId)
        {
            if (from >= to)
            {
                throw new DomainException("From date must be earlier than To date.");
            }
            
            if(from < DateTime.Now)
            {
                throw new DomainException("From date cannot be in the past.");
            }

            if (to < DateTime.Now)
            {
                throw new DomainException("To date cannot be in the past.");
            }
            if (roomId == Guid.Empty)
            {
                throw new DomainException("Room ID cannot be empty.");
            }
            if (userId == Guid.Empty)
            {
                throw new DomainException("User ID cannot be empty.");
            }
        }

        public void Update(DateTime from, DateTime to, Guid roomId, Guid userId)
        {
            if (from >= to)
            {
                throw new ArgumentException("From date must be earlier than To date.");
            }
            
            IsAuthorized(userId);

            From = from;
            To = to;
            RoomId = roomId;
        }

        public void IsAuthorized(Guid userId)
        {
            if (UserId != userId)
            {
                throw new UnauthorizedAccessException("You are not authorized to perform this action on this reservation.");
            }
        }
    }
}
