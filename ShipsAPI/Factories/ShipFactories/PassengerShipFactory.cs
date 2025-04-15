
using ShipsAPI.Models;
using ShipsAPI.Models.Passengers;
using ShipsAPI.Models.Ships;

namespace ShipsAPI.Factories.ShipFactories
{

    public class PassengerShipFactory : IShipFactory<NewPassengerShipDto>
    {
        public Ship CreateShip(NewPassengerShipDto dto)
        {
            var passengers = PassengersFromDtoConverter(dto.Passengers).ToList();

            return new PassengerShip
            {
                IMO = dto.IMO,
                Name = dto.Name,
                Length = dto.Length,
                Width = dto.Width,
                ShipType = dto.ShipType,
                Passengers = passengers,
                PeopleCounter = passengers.Count
            };
        }
     

        private IEnumerable<Passenger> PassengersFromDtoConverter(IEnumerable<PassengerDto> passengersDto)
        {
            return passengersDto.Select((p, index) => new Passenger
            {
                Id = index,
                Name = p.Name,
                Surname = p.Surname,             
            });
        }
    }
}
