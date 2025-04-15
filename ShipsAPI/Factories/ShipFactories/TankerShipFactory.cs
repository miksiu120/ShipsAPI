using ShipsAPI.Models;
using ShipsAPI.Models.Ships;
using ShipsAPI.Models.Tanks;

namespace ShipsAPI.Factories.ShipFactories
{
    public class TankerShipFactory : IShipFactory<NewTankerShipDto>
    {
        public Ship CreateShip(NewTankerShipDto dto)
        {
            return new TankerShip
            {
                IMO = dto.IMO,
                Name = dto.Name,
                Length = dto.Length,
                Width = dto.Width,
                ShipType = dto.ShipType,
                Tanks = dto.Tanks
            };
        }
    }
}
