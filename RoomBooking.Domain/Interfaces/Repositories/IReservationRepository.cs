using RoomsReservation.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomsReservation.Domain.Interfaces.Repositories
{
    public interface IReservationRepository
    {
        public Task<Guid> Create(Reservation reservation);
        public Task<bool> Update(Reservation reservation);
        public Task<bool> Delete(Guid roomId);
        public Task<Reservation?> GetById(Guid id);
        public Task<bool> UserHasReservation(Guid userId, DateTime from, DateTime to, Guid? excludedReservationId);
    }
}
