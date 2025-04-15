using ShipsAPI.Models;
using ShipsAPI.Models.Ships;

namespace ShipsAPI.Factories.ShipFactories
{
    public interface IShipFactory<TDto> where TDto : NewShipDtoBase
    {
        Ship CreateShip(TDto dto);
    }
}
