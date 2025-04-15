using ShipsAPI.Exceptions;
using ShipsAPI.Models;
using ShipsAPI.Models.Ships;
using ShipsAPI.Repositories;

namespace ShipsAPI.Services.Ships
{
    public interface IShipService
    {
        List<Ship> GetAllShips();
        Ship GetShipByIMO(string imo);   
        Ship AddNewShip(NewShipDtoBase ship);
        void EditShip(Ship ship);
    }

    public class ShipService:IShipService
    {
        private readonly IShipRepository _shipRepository;
        private readonly IShipFactoryProvider _shipfactoryProvider;

        public ShipService(IShipRepository shipRepository, IShipFactoryProvider shipFactoryProvider)
        {

            _shipRepository = shipRepository;
            _shipfactoryProvider = shipFactoryProvider;

        }

        public List<Ship> GetAllShips()
        {
            return _shipRepository.GetAllShips();
        }
        public Ship GetShipByIMO(string imo)
        {
            var ship = _shipRepository.GetShipByIMO(imo);

            if (ship is null)
            {
                throw new ShipNotFoundException($"Ship with IMO {imo} doest not exists ");
            }

            return ship;
        }

        public Ship AddNewShip(NewShipDtoBase shipDto)
        {
            var existingShip = _shipRepository.GetShipByIMO(shipDto.IMO);

            if (existingShip is not null)
            {
                throw new InvalidShipOperationException($"Ship with IMO: ${shipDto.IMO} already exists");
            }

            if (shipDto.Width <= 0)
            {
                throw new InvalidShipOperationException($"Ship must have positive width ");
            }

            if (shipDto.Length <= 0)
            {
                throw new InvalidShipOperationException($"Ship must have positive length");
            }

            if (!IMOService.IsCorrect(shipDto.IMO))
            {
                throw new InvalidShipOperationException($"IMO identifier is not correct");
            }

            var createdShip = _shipfactoryProvider.CreateShip(shipDto);
            _shipRepository.InsertNewShip(createdShip);

            return createdShip;
        }

        public void EditShip(Ship ship)
        {
            var existingShip = _shipRepository.GetShipByIMO(ship.IMO);

            if (existingShip is not null)
            {
                throw new ShipNotFoundException($"Ship with IMO: ${ship.IMO} doest not exist and cannot be edited ");
            }
            _shipRepository.UpdateShip(ship);
        }

    }
}
