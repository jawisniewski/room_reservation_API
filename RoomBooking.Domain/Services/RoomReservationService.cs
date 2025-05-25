using RoomBooking.Domain.Entitis.Room;
using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.Interfaces.Services;
using RoomsReservation.Domain.Entitis;
using RoomsReservation.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Services
{
    public class RoomReservationService : IRoomReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public RoomReservationService(IReservationRepository reservationRepository, IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Guid> ReserveRoom(Guid roomId, DateTime from, DateTime to, Guid userId)
        {
            var room = await _roomRepository.GetByIdAsync(roomId)
                ?? throw new DomainException("Room not found");

            room.IsReservationWithinLimits(from, to);

            var isAvailable = await _roomRepository.IsAvailableAsync(roomId, from, to);

            if (!isAvailable)
            {
                throw new DomainException("Room is not available");
            }

            var userHasAReservation = await _reservationRepository.UserHasReservation(userId, from, to, null);

            if (userHasAReservation)
            {
                throw new DomainException("User already has a reservation during this time");
            }

            var reservation = new Reservation(from, to, roomId, userId);

            var result = await _reservationRepository.Create(reservation);

            return result;
        }

        public async Task<bool> UpdateReservation(Guid reservationId, Guid roomId, DateTime from, DateTime to, Guid userId)
        {
            var existingReservation = await _reservationRepository.GetById(reservationId)
                ?? throw new DomainException("Reservation not found");

            var isAvailable = await _roomRepository.IsAvailableAsync(roomId, from, to, reservationId);

            if (!isAvailable)
            {
                throw new DomainException("Room is not available");
            }

            var userHasAReservation = await _reservationRepository.UserHasReservation(userId, from, to, reservationId);

            if (userHasAReservation)
            {
                throw new DomainException("User already has a reservation during this time");
            }

            existingReservation.Update(from, to, roomId, userId);

            var result = await _reservationRepository.Update(existingReservation);

            return result;
        }

        public async Task<bool> CancelReservation(Guid reservationId, Guid userId)
        {
            var reservation = await _reservationRepository.GetById(reservationId)
                ?? throw new DomainException("Reservation not found");

            reservation.IsAuthorized(userId);

            var result = await _reservationRepository.Delete(reservationId);

            return result;
        }
    }
}
