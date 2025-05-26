using Microsoft.EntityFrameworkCore;
using RoomBooking.Application.DTOs.Room;
using RoomBooking.Domain.Entitis.Room;
using RoomBooking.Infrastructure.Configs;
using RoomBooking.Services.Interfaces.Repositories;
using RoomsReservation.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Infrastructure.Repositories
{
    public class RoomRepository : IRoomRepository, IRoomQueryRepository
    {
        private readonly DbSet<Room> _rooms;
        private readonly AppDbContext _context;
        public RoomRepository(AppDbContext context)
        {
            _context = context;
            _rooms = _context.Set<Room>();
        }
        public async Task<Guid> CreateAsync(Room room)
        {
            await _rooms.AddAsync(room);
            await _context.SaveChangesAsync();

            return room.Id;
        }

        public async Task<bool> DeleteAsync(Guid roomId)
        {
            var room = await _rooms.FirstOrDefaultAsync(r => r.Id == roomId).ConfigureAwait(false);

            if (room == null)
                return false;

            _rooms.Remove(room);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Room>> GetAvailableRoomsAsync(DateTime from, DateTime to, RoomFilters roomFilter)
        {
            var query = _rooms.AsNoTracking().AsQueryable();

            ApplyRoomQueryFilters(roomFilter, ref query);
            FilterRoomsByAvailability(from, to, null, ref query);

            return await query
                .ToListAsync()
                .ConfigureAwait(false);

        }

        public async Task<Room?> GetByIdAsync(Guid id)
        {
            return await _rooms.Include(x => x.Equipments)
                .FirstOrDefaultAsync(r => r.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<Room?> GetByNameAsync(string name)
        {
            return await _rooms.AsNoTracking()
                .FirstOrDefaultAsync(r => r.Name == name)
                .ConfigureAwait(false);
        }

        public async Task<bool> IsAvailableAsync(Guid room, DateTime from, DateTime to, Guid? excludedReservationId = null)
        {
            var query = _rooms.AsNoTracking()
                .Where(r => r.Id == room)
                .Include(r => r.Reservations)
                .AsQueryable();

            FilterRoomsByAvailability(from, to, excludedReservationId, ref query);

            return await query.AnyAsync();
        }

        public async Task<bool> UpdateAsync(Room room)
        {
            _rooms.Update(room);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        private void FilterRoomsByAvailability(DateTime from, DateTime to, Guid? excludedReservationId, ref IQueryable<Room> roomQuery)
        {
            roomQuery = roomQuery.Where(r => !r.Reservations.Any(reservation =>
                (reservation.Id != excludedReservationId) && (reservation.From < to && reservation.To > from)));
        }

        private void ApplyRoomQueryFilters(RoomFilters roomFilter, ref IQueryable<Room> roomsQuery)
        {
            if (roomFilter == null)
                return;

            if (roomFilter.Capacity != null)
                roomsQuery = roomsQuery.Where(r => r.Capacity >= roomFilter.Capacity);
            if (roomFilter.Name != null)
                roomsQuery = roomsQuery.Where(r => r.Name.ToLower().Contains(roomFilter.Name.ToLower()));
            if (roomFilter.TableCount != null)
                roomsQuery = roomsQuery.Where(r => r.TableCount >= roomFilter.TableCount);
            if (roomFilter.Layout != null)
                roomsQuery = roomsQuery.Where(r => r.Layout == roomFilter.Layout);
            if (roomFilter.Equipments != null)
                roomsQuery = roomsQuery.Where(r => r.Equipments.All(e => roomFilter.Equipments.Any(rfe => rfe.Name == e.Type.Name)));
        }
    }
}
