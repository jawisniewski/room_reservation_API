using Microsoft.EntityFrameworkCore;
using RoomBooking.Infrastructure.Configs;
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
        private readonly AppDbContext _context;
        private readonly DbSet<Reservation> _reservations;
        public ReservationRepository(AppDbContext context)
        {
            _context = context;
            _reservations = _context.Set<Reservation>();
        }
        public async Task<Guid> Create(Reservation reservation)
        {
            await _reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return reservation.Id;
        }

        public async Task<bool> Delete(Guid roomId)
        {
            var reservation = await _reservations.FirstOrDefaultAsync(r => r.Id == roomId).ConfigureAwait(false);

            if (reservation == null)
                return false;

            _reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<Reservation?> GetById(Guid id)
        {
            return _reservations
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Update(Reservation reservation)
        {
            if (reservation.Id == Guid.Empty)
                return false;

            _reservations.Update(reservation);
            await _context.SaveChangesAsync();

            return true;
        }

        public Task<bool> UserHasReservation(Guid userId, DateTime from, DateTime to, Guid? excludedReservationId)
        {
            return _reservations
                .AnyAsync(r => r.UserId == userId &&
                               r.From < to &&
                               r.To > from &&
                               r.Id != excludedReservationId);
        }
    }
}
