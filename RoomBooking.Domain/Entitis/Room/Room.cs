using RoomBooking.Domain.Exceptions;
using RoomBooking.Domain.ValueObjects;
using RoomsReservation.Domain.Entitis;
using RoomsReservation.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.Entitis.Room
{
    public class Room
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public int Capacity { get; private set; }
        public int TableCount { get; private set; }
        public RoomLayoutEnum Layout { get; private set; }
        public virtual IEnumerable<Reservation> Reservations { get; private set; } = [];
        public virtual RoomReservationLimit? ReservationLimit { get; private set; }
        public  IEnumerable<Equipment> Equipments { get; private set; }
        private Room() { }
        private Room(string name, int capacity, int tableCount, RoomLayoutEnum roomLayout, List<Equipment> equipments, RoomReservationLimit reservationLimit)
        {
            Name = name;
            Capacity = capacity;
            TableCount = tableCount;
            Layout = roomLayout;
            Equipments = equipments;
            ReservationLimit = reservationLimit;
        }

        public static Room Create(string name, int capacity, int tableCount, RoomLayoutEnum roomLayout, List<Equipment> equipments, RoomReservationLimit? reservationLimit)
        {
            Validate(name, capacity, tableCount, roomLayout, equipments);

            return new Room(name, capacity, tableCount, roomLayout, equipments, reservationLimit);
        }

        public void Update(string name, int capacity, int tableCount, RoomLayoutEnum roomLayout, List<Equipment> equipments, RoomReservationLimit? reservationLimit)
        {
            if(Id == Guid.Empty)
                throw new DomainException("Room ID cannot be empty. Ensure the room is properly initialized.");
            
            Validate(name, capacity, tableCount, roomLayout, equipments);

            Name = name.Trim(); 
            Capacity = capacity;
            TableCount = tableCount;
            Layout = roomLayout;
            Equipments = equipments;
            ReservationLimit = reservationLimit;

        }
        private static void Validate(string name, int capacity, int tableCount, RoomLayoutEnum roomLayout, List<Equipment> equipments)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Room name cannot be empty");
            if (capacity <= 0)
                throw new DomainException("Capacity must be positive");
            if (tableCount < 0)
                throw new DomainException("Table count cannot be negative");

            ValidateLayout(roomLayout, tableCount, capacity);
            ValidateEquipments(equipments);
        }

        private static void ValidateLayout(RoomLayoutEnum roomLayout, int tableCount, int capacity)
        {
            switch (roomLayout)
            {
                case RoomLayoutEnum.Boardroom:
                    if (tableCount < 1)
                        throw new DomainException("BOARDROOM must have at least 1 table.");
                    break;
                case RoomLayoutEnum.Classroom:
                    if (tableCount <= 1 || capacity % 2 != 0)
                        throw new DomainException("CLASSROOM must have more then one table and even number of seats.");
                    break;
                case RoomLayoutEnum.Theater:
                    if (tableCount != 0)
                        throw new DomainException("THEATER must not have any tables.");
                    break;
            }
        }

        private static void ValidateEquipments(IEnumerable<Equipment> equipments)
        {
            if (equipments == null || !equipments.Any())
                throw new DomainException("At least one equipment is required.");

            if (equipments.Any(x => x.Type == EquipmentType.Videoconferencing) &&
                !(equipments.Any(x => x.Type == EquipmentType.Projector || x.Type == EquipmentType.Screen)))
            {
                throw new DomainException("Videoconferencing set requires projector or screen.");
            }
        }
        public void IsReservationWithinLimits(DateTime from, DateTime to)
        {
            if (ReservationLimit is null)
                return;

            var reservationDuration = (to - from).TotalMinutes;

            if (reservationDuration < ReservationLimit.MinTime || reservationDuration > ReservationLimit.MaxTime)
            {
                throw new DomainException($"Reservation duration must be between {ReservationLimit.MinTime} and {ReservationLimit.MaxTime} minutes.");
            }
        }
    }
}
