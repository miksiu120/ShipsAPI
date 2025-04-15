using ShipsAPI.Exceptions;
using ShipsAPI.Factories.ShipFactories;
using ShipsAPI.Models;
using ShipsAPI.Models.Passengers;
using ShipsAPI.Models.Ships;
using ShipsAPI.Models.Tanks;
using ShipsAPI.Repositories;
using ShipsAPI.Services.Ships;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShipsAPI.Tests
{
    public class PassengerShipServiceTests
    {
        private readonly IShipRepository shipRepository;
        private readonly IShipFactory<NewPassengerShipDto> passengerFactory;
        private readonly IShipFactory<NewTankerShipDto> tankerFactory;
        private readonly IShipFactoryProvider shipFactoryProvider;
        private readonly IShipService shipService;
        private readonly IPassengerShipService passengerShipService;

        private const string CORRECT_IMO = "9074729";
        private const string INCORRECT_IMO = "1111111";

        public PassengerShipServiceTests()
        {
            shipRepository = new ShipRepository();
            passengerFactory = new PassengerShipFactory();
            tankerFactory = new TankerShipFactory();
            shipFactoryProvider = new ShipFactoryProvider(passengerFactory, tankerFactory);
            shipService = new ShipService(shipRepository, shipFactoryProvider);
            passengerShipService = new PassengerShipService(shipRepository);

            AddTestPassengerShip();
        }

        private void AddTestPassengerShip()
        {
            var newPassengerShip = new NewPassengerShipDto
            {
                IMO = CORRECT_IMO,
                Name = "Fast Ferry",
                Width = 25.0,
                Length = 150.0,
                Passengers = new List<PassengerDto>
            {
                new PassengerDto { Name = "Anna", Surname = "Nowak" },
                new PassengerDto { Name = "Piotr", Surname = "Kowalski" }
            }
            };

            shipService.AddNewShip(newPassengerShip);
        }


        [Theory]
        [InlineData(-1)]
        public void GetPassenger_WithInvalidPassengerId_ThrowsPassengerNotFoundException(int passengerId)
        {
            // assert
            Assert.Throws<PassengerNotFoundException>(() => passengerShipService.GetPassenger(CORRECT_IMO, passengerId));
        }

        [Theory]
        [InlineData(0)]
        public void GetPassenger_WithInvalidPassengerId_PassengerReturned(int passengerId)
        {
            // act 
            var passenger = passengerShipService.GetPassenger(CORRECT_IMO, passengerId);

            // assert
            Assert.NotNull(passenger);
        }

        [Theory]
        [InlineData("Jan", "Pawel")]
        public void AddPassenger_WithValidPassengerData_PassengerIsAdded(string name, string surname)
        {
            // arrange 
            var passengerDto = new PassengerDto { Name = name, Surname = surname };
            int before = ((PassengerShip)shipService.GetShipByIMO(CORRECT_IMO)).Passengers.Count;

            // act 
            passengerShipService.AddNewPassenger(CORRECT_IMO, passengerDto);
            int after = ((PassengerShip)shipService.GetShipByIMO(CORRECT_IMO)).Passengers.Count;

            // assert 
            Assert.Equal(before + 1, after);
        }

        [Theory]
        [InlineData("Jan", "")]
        [InlineData("", "")]
        [InlineData("Pawel", "")]
        public void AddPassenger_WithInvalidPassengerData_PassengerIsAdded(string name, string surname)
        {
            // arrange 
            var passengerDto = new PassengerDto { Name = name, Surname = surname };
            int before = ((PassengerShip)shipService.GetShipByIMO(CORRECT_IMO)).Passengers.Count;

            // act 
            passengerShipService.AddNewPassenger(CORRECT_IMO, passengerDto);
            int after = ((PassengerShip)shipService.GetShipByIMO(CORRECT_IMO)).Passengers.Count;

            // assert 
            Assert.Equal(before + 1, after);
        }

        [Theory]
        [InlineData(0, "Jan", "Kowalski")]
        [InlineData(0, "Adam", "Krupniewicz")]
        public void UpdatePassenger_WithValidData_PassengerIsUpdated(int passengerId, string name, string surname)
        {
            // arrange 
            var passengerDto = new PassengerDto { Name = name, Surname = surname };

            // act 
            passengerShipService.UpdatePassenger(CORRECT_IMO, passengerId, passengerDto);

            // assert 
            var updatedPassenger = ((PassengerShip)shipService.GetShipByIMO(CORRECT_IMO)).Passengers
                .FirstOrDefault(p => p.Id == passengerId);

            Assert.NotNull(updatedPassenger);
            Assert.Equal(name, updatedPassenger.Name);
            Assert.Equal(surname, updatedPassenger.Surname);
        }

        [Theory]
        [InlineData(0)]
        public void DeletePassenger_WithValidPassengerId_PassengerIsRemoved(int passengerId)
        {
            // arrange
            int before = ((PassengerShip)shipService.GetShipByIMO(CORRECT_IMO)).Passengers.Count;

            // act
            passengerShipService.DeletePassenger(CORRECT_IMO, passengerId);
            int after = ((PassengerShip)shipService.GetShipByIMO(CORRECT_IMO)).Passengers.Count;

            // assert
            Assert.Equal(before - 1, after);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(99999999)]
        public void DeletePassenger_WidthInvalidPassengerId_ThrowsInvalidPassengerException(int passengerid)
        {
            Assert.Throws<InvalidPassengerException>(() => passengerShipService.DeletePassenger(CORRECT_IMO,passengerid));
        }
    }
}
