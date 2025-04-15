namespace ShipsAPI.Models.Ships
{
    public abstract class Ship
    {
        public required string IMO { get; set; }
        public required string Name { get; set; }
        public required double Width { get; set; }
        public required double Length { get; set; }
        public required ShipType ShipType { get; set; }
    }
}
