using ShipsAPI.Models.Passengers;
using ShipsAPI.Models.Ships;
using ShipsAPI.Models.Tanks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace ShipsAPI.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "shipType")]
    [JsonDerivedType(typeof(NewPassengerShipDto), typeDiscriminator: "Passenger")]
    [JsonDerivedType(typeof(NewTankerShipDto), typeDiscriminator: "Tanker")]
    public abstract class NewShipDtoBase
    {
        public required string IMO { get; set; }
        public required string Name { get; set; }
        public required double Width { get; set; }
        public required double Length { get; set; }
        public abstract ShipType ShipType { get; }
    }

    public class NewPassengerShipDto : NewShipDtoBase
    {
        public override ShipType ShipType => ShipType.Passenger;
        public required List<PassengerDto> Passengers { get; set; }
    }

    public class NewTankerShipDto : NewShipDtoBase
    {
        public override ShipType ShipType => ShipType.Tanker;
        public required List<Tank> Tanks { get; set; }
    }
}
