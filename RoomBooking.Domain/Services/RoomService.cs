using RoomBooking.Domain.Entitis.Room;
using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.Interfaces.Services;
using RoomBooking.Domain.ValueObjects;
using RoomsReservation.Domain.Enums;
using RoomsReservation.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Guid> CreateAsync(string name, int capacity, int tableCount, RoomLayoutEnum roomLayout, List<Equipment> equipments, RoomReservationLimit? reservationLimit)
        {
            var room = Room.Create(name, capacity, tableCount, roomLayout, equipments, reservationLimit);
            var existingRoom = await _roomRepository.GetByNameAsync(name).ConfigureAwait(false);

            if (existingRoom != null)
            {
                throw new DomainException($"Room with name '{name}' already exists.");
            }
            return await _roomRepository.CreateAsync(room).ConfigureAwait(false);
        }

        public async Task<bool> DeleteAsync(Guid roomId)
        {
            var room = await _roomRepository.GetByIdAsync(roomId).ConfigureAwait(false);

            if (room == null)
            {
                throw new DomainException("Room not found");
            }

            return await _roomRepository.DeleteAsync(roomId).ConfigureAwait(false);
        }

        public async Task<bool> UpdateAsync(Guid roomId, string name, int capacity, int tableCount, RoomLayoutEnum roomLayout, List<Equipment> equipments, RoomReservationLimit? reservationLimit)
        {
            var room = await _roomRepository.GetByIdAsync(roomId).ConfigureAwait(false);

            if (room == null)
            {
                throw new DomainException("Room not found");
            }
            var existingRoom = await _roomRepository.GetByNameAsync(name).ConfigureAwait(false);

            if (existingRoom != null && roomId != existingRoom.Id)
            {
                throw new DomainException($"Room with name '{name}' already exists.");
            }

            room.Update(name, capacity, tableCount, roomLayout, equipments, reservationLimit);

            return await _roomRepository.UpdateAsync(room).ConfigureAwait(false);
        }

    }
}
