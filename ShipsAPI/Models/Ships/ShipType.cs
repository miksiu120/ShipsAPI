using System.Text.Json.Serialization;

namespace ShipsAPI.Models.Ships
{
    public enum ShipType
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        Tanker,
        [JsonConverter(typeof(JsonStringEnumConverter))]
        Passenger
    }
}
 