using ShipsAPI.Factories.ShipFactories;
using ShipsAPI.Models;
using ShipsAPI.Models.Ships;


public interface IShipFactoryProvider
{
    Ship CreateShip(NewShipDtoBase dto);
}

public class ShipFactoryProvider:IShipFactoryProvider
{
    private readonly Dictionary<ShipType, object> _factories;

    public ShipFactoryProvider(
        IShipFactory<NewPassengerShipDto> passengerFactory,
        IShipFactory<NewTankerShipDto> tankerFactory)
    {
        _factories = new Dictionary<ShipType, object>
        {
            { ShipType.Passenger, passengerFactory },
            { ShipType.Tanker, tankerFactory }
        };
    }

    public Ship CreateShip(NewShipDtoBase dto)
    {
        var factory = _factories[dto.ShipType];
        var method = factory.GetType().GetMethod("CreateShip");
        return (Ship)method.Invoke(factory, new object[] { dto });
    }
}
