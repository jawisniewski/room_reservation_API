using RoomBooking.Domain.Common;

namespace RoomBooking.Domain.ValueObjects
{
    public class EquipmentType : ValueObject
    {
        public string Name { get; private set; }

        private EquipmentType() { }

        public static readonly EquipmentType Projector = new("Projector");
        public static readonly EquipmentType Whiteboard = new("Whiteboard");
        public static readonly EquipmentType Videoconferencing = new("Videoconferencing");
        public static readonly EquipmentType Screen = new("Screen");

        private EquipmentType(string name)
        {
            Name = name;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
