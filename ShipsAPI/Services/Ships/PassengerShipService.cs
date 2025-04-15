using ShipsAPI.Exceptions;
using ShipsAPI.Models.Passengers;
using ShipsAPI.Models.Ships;
using ShipsAPI.Repositories;

namespace ShipsAPI.Services.Ships
{
    public interface IPassengerShipService
    {

        Passenger GetPassenger(string imo, int passengerId);
        IEnumerable<Passenger> GetPassengers(string imo);
        Passenger AddNewPassenger(string imo, PassengerDto passenger);
        IEnumerable<Passenger> AddNewPassengers(string imo, IEnumerable<PassengerDto> passengers);
        void UpdatePassenger(string imo,int passengerId ,PassengerDto passenger);
        void DeletePassenger(string imo, int passengerId);
    }

    public class PassengerShipService : IPassengerShipService
    {
        private readonly IShipRepository _shipRepository;

        public PassengerShipService(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public Passenger GetPassenger(string imo, int passengerId)
        {
            var ship = _shipRepository.GetShipByIMO(imo) as PassengerShip;

            if (ship is null)
            {
                throw new ShipNotFoundException($"Ship with IMO {imo} does not exist or is not a passenger ship");
            }

            return ship.GetPassenger(passengerId); ;
        }

        public IEnumerable<Passenger> GetPassengers(string imo)
        {
            var ship = _shipRepository.GetShipByIMO(imo) as PassengerShip;

            if (ship is null)
            {
                throw new ShipNotFoundException($"Ship with IMO {imo} does not exist or is not a passenger ship");
            }

            return ship.Passengers ;
        }

        public Passenger AddNewPassenger(string imo, PassengerDto passengerDto)
        {
            var ship = _shipRepository.GetShipByIMO(imo) as PassengerShip;

            if (ship is null)
            {
                throw new ShipNotFoundException($"Ship with IMO {imo} does not exist or is not a passenger ship");
            }

            return ship.AddNewPassenger(passengerDto); ;
        }

        public IEnumerable<Passenger> AddNewPassengers(string imo, IEnumerable<PassengerDto> passengers)
        {
            var ship = _shipRepository.GetShipByIMO(imo) as PassengerShip;
                               
            if(ship is null)
            {
               throw new ShipNotFoundException($"Ship with IMO {imo} does not exist or is not a passenger ship");
            }

            return ship.AddNewPassengers(passengers); 
        }



        public void UpdatePassenger(string imo,int id, PassengerDto passenger)
        {
            var ship = _shipRepository.GetShipByIMO(imo) as PassengerShip;

            if (ship is null)
            {
                throw new ShipNotFoundException($"Ship with IMO {imo} does not exist or is not a passenger ship");
            }

            ship.EditPassenger(id, passenger);
        }

        public void DeletePassenger(string imo, int passengerId)
        {
            var ship = _shipRepository.GetShipByIMO(imo) as PassengerShip;

            if (ship is null)
            {
                throw new ShipNotFoundException($"Ship with IMO {imo} does not exist or is not a passenger ship");
            }

            ship.RemovePassenger(passengerId);
        }
    }

}
