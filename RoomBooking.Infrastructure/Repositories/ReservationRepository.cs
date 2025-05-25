using RoomsReservation.Domain.Entitis;
using RoomsReservation.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Infrastructure.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        public Task<Guid> Create(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid roomId)
        {
            throw new NotImplementedException();
        }

        public Task<Reservation?> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UserHasReservation(Guid userId, DateTime from, DateTime to, Guid? excludedReservationId)
        {
            throw new NotImplementedException();
        }
    }
}
