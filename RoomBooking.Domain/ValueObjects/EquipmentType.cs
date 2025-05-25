using RoomBooking.Domain.Common;
using RoomBooking.Domain.DI;
using RoomBooking.Domain.Exceptions;

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

        public static EquipmentType FromName(string name)
        {
            return name.ToLower().Trim() switch
            {
                "projector" => Projector,
                "whiteboard" => Whiteboard,
                "videoconferencing" => Videoconferencing,
                "screen" => Screen,
                _ => throw new DomainException($"Unknown equipment type")

            };
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
