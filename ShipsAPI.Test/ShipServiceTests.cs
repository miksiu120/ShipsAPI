using ShipsAPI.Exceptions;
using ShipsAPI.Factories.ShipFactories;
using ShipsAPI.Models;
using ShipsAPI.Models.Passengers;
using ShipsAPI.Models.Ships;
using ShipsAPI.Repositories;
using ShipsAPI.Services;
using ShipsAPI.Services.Ships;

namespace ShipsAPI.Test
{
    public class ShipServiceTests
    {

        IShipRepository _shipRepository;
        IShipFactory<NewPassengerShipDto> _passengerFactory;
        IShipFactory<NewTankerShipDto> _tankerFactory;
        IShipFactoryProvider _shipFactoryProvider;
        IShipService _shipService;
        public ShipServiceTests()
        {
            _shipRepository = new ShipRepository();
            _passengerFactory = new PassengerShipFactory();
            _tankerFactory = new TankerShipFactory();
            _shipFactoryProvider = new ShipFactoryProvider(_passengerFactory, _tankerFactory);
            _shipService = new ShipService(_shipRepository, _shipFactoryProvider);
        }


        public static IEnumerable<object[]> ValidShipData =>
       new List<object[]>
       {
            new object[]
            {
                new NewPassengerShipDto
                {
                    IMO = "9074729",
                    Name = "Passenger Ship",
                    Width = 30.0,
                    Length = 200.0,
                    Passengers = new List<PassengerDto>
                    {
                    }
                }
            },
            new object[]
            {
                new NewPassengerShipDto
                {
                    IMO = "1234567",
                    Name = "Fast Ferry",
                    Width = 25.0,
                    Length = 150.0,
                    Passengers = new List<PassengerDto>
                    {
                        new PassengerDto { Name = "Anna", Surname = "Nowak" },
                        new PassengerDto { Name = "Piotr", Surname = "Kowalski" }
                    }
                }
            }
       };

        [Theory]
        [MemberData(nameof(ValidShipData))]
        public void AddValidShip_ForShip_ReturnsAddedShip(NewPassengerShipDto newShipDto)
        {
            // act             
            Ship createdResult =  _shipService.AddNewShip(newShipDto);

            // assert 
            Assert.Equal(createdResult.Length, newShipDto.Length);
            Assert.Equal(createdResult.Width, newShipDto.Width);
            Assert.Equal(createdResult.Name, newShipDto.Name);
            Assert.Equal(createdResult.ShipType, newShipDto.ShipType);
            Assert.Equal(newShipDto.Passengers.Count, newShipDto.Passengers.Count);

        }

        [Theory]
        [InlineData("9074729", 0.0, 30.1)]
        [InlineData("9074729", 15.0, -1.0)]
        [InlineData("9074729", -1.0, -1.0)]
        public void AddNewShip_InvalidDimensions_ThrowsException(string imo, double width, double length)
        {
            var dto = new NewPassengerShipDto
            {
                IMO = imo,
                Name = "Passenger Ship",
                Width = width,
                Length = length,
                Passengers = new List<PassengerDto>()
            };

            Assert.Throws<InvalidShipOperationException>(() => _shipService.AddNewShip(dto));
        }

        [Theory]
        [InlineData("11111111111", 20.0, 30.0)]
        [InlineData("abcdefg", 25.0, 40.0)]
        public void AddNewShip_InvalidIMO_ThrowsException(string imo, double width, double length)
        {
            var dto = new NewPassengerShipDto
            {
                IMO = imo,
                Name = "Passenger Ship",
                Width = width,
                Length = length,
                Passengers = new List<PassengerDto>()
            };

            Assert.Throws<InvalidShipOperationException>(() => _shipService.AddNewShip(dto));
        }
    }
}
