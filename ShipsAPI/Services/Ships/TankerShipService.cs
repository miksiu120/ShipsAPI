using ShipsAPI.Exceptions;
using ShipsAPI.Models.Ships;
using ShipsAPI.Models.Tanks;
using ShipsAPI.Repositories;

namespace ShipsAPI.Services.Ships
{

    public interface ITankerShipService
    {
        void FuelUpTank(string imo, int tankId, FuelUpDto fuelUpDto);
        void EmptyTank(string imo, int tankId);
    }

    public class TankerShipService: ITankerShipService
    {
        private readonly IShipRepository _shipRepository;

        public TankerShipService(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public void FuelUpTank(string imo, int tankId, FuelUpDto fuelUpDto)
        {

            if (fuelUpDto.AmountToFuelUpInLitres < 0)
            {
                throw new Exception($"You cannot fuelup tank with negative amount of fuel");
            }

            var existingShip = _shipRepository.GetShipByIMO(imo) as TankerShip;

            if (existingShip is null)
            {
                throw new ShipNotFoundException($"Ship with IMO: {imo} does not exist and cannot be fueled up");
            }

            if (existingShip.ShipType != ShipType.Tanker)
            {
                throw new InvalidShipOperationException($"Only tankers can be fueledUp");
            }

            existingShip.fuelUpTanker(tankId,fuelUpDto);                   
        }

        public void EmptyTank(string imo, int tankId)
        {
            var existingShip = _shipRepository.GetShipByIMO(imo) as TankerShip;

            if (existingShip is null)
            {
                throw new ShipNotFoundException($"Ship with IMO: {imo} does not exist and cannot be empted");
            }

            existingShip.emptyTanker(tankId);

        }
    }
}
