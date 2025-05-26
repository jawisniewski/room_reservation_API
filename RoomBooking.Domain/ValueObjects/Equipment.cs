using RoomBooking.Domain.Common;
using RoomBooking.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Domain.ValueObjects
{
    public class Equipment : ValueObject
    {
        public EquipmentType Type { get; private set; }
        public int Quantity { get; private set; }

        private Equipment() { }

        public  Equipment(EquipmentType name, int quantity)
        {
            ValidateQuantity(quantity);

            Type = name;
            Quantity = quantity;
        }
       
        private void ValidateQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Quantity must be positive");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Quantity;
        }
    }

}
