using RoomBooking.Domain.Common;
using RoomBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.ValueObjects
{
    public class RoomReservationLimit : ValueObject
    {
        public int MinTime { get; private set; }
        public int MaxTime { get; private set; }

        private RoomReservationLimit() { }

        public RoomReservationLimit(int minTime, int maxTime)
        {
            Validate(minTime, maxTime);

            MinTime = minTime;
            MaxTime = maxTime;
        }

        private void Validate(int minTime, int maxTime)
        {
            if (minTime < 0)
                throw new DomainException("Minimal reservation time must be positive.");

            if (maxTime < 0)
                throw new DomainException("Maximal reservation time must be positive.");

            if (minTime > maxTime)
                throw new DomainException("Maximal reservation time must be bigger then minimal.");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return MinTime;
            yield return MaxTime;
        }
    }

}
