using ShipsAPI.Exceptions;
using ShipsAPI.Factories.ShipFactories;
using ShipsAPI.Models;
using ShipsAPI.Models.Ships;
using ShipsAPI.Models.Tanks;
using ShipsAPI.Repositories;
using ShipsAPI.Services.Ships;

namespace ShipsAPI.Tests
{
    public class TankerShipServiceTests
    {
        private readonly IShipRepository shipRepository;
        private readonly IShipFactory<NewPassengerShipDto> passengerFactory;
        private readonly IShipFactory<NewTankerShipDto> tankerFactory;
        private readonly IShipFactoryProvider shipFactoryProvider;
        private readonly IShipService shipService;
        private readonly ITankerShipService tankerShipService;

        private const string CORRECT_IMO = "9074729";

        public TankerShipServiceTests()
        {
            shipRepository = new ShipRepository();
            passengerFactory = new PassengerShipFactory();
            tankerFactory = new TankerShipFactory();
            shipFactoryProvider = new ShipFactoryProvider(passengerFactory, tankerFactory);
            shipService = new ShipService(shipRepository, shipFactoryProvider);
            tankerShipService = new TankerShipService(shipRepository);

            var newTankerShip = new NewTankerShipDto
            {
                IMO = CORRECT_IMO,
                Name = "Pilsudski",
                Width = 213.23,
                Length = 600.13,
                Tanks = new List<Tank>
            {
                new Tank
                {
                    Id = 0,
                    CapacityInLitres = 10000,
                    ActualFuelType = FuelType.Diesel,
                    CurrentAmountInLitres = 2000
                }
            }
            };

            shipService.AddNewShip(newTankerShip);
        }

        [Theory]
        [InlineData(100, 0)]
        [InlineData(1, 0)]
        [InlineData(8000, 0)]
        [InlineData(0.5, 0)]

        public void FuelUpTank_ValidAmount_TankFueledUp( double fuelToAdd, int tankId)
        {
            // arrange
            var amountBefore = ((TankerShip)shipService.GetShipByIMO(CORRECT_IMO)).Tanks[tankId].CurrentAmountInLitres;

            FuelUpDto fuelUpDto = new FuelUpDto
            {
                AmountToFuelUpInLitres = fuelToAdd,
                FuelType = FuelType.Diesel
            };


            // act
            tankerShipService.FuelUpTank(CORRECT_IMO, tankId, fuelUpDto);

            // assert
            var amountAfter = ((TankerShip)shipService.GetShipByIMO(CORRECT_IMO)).Tanks[tankId].CurrentAmountInLitres;
            Assert.Equal(amountBefore + fuelToAdd, amountAfter);
        }

        [Theory]
        [InlineData(10001, 0)]
        [InlineData(8001, 0)]
        public void FuelUpTank_WrongFuelValue_ThrowsOverflowException(double fuelToAdd, int tankId)
        {
            //arrange
            FuelUpDto fuelUpDto = new FuelUpDto
            {
                AmountToFuelUpInLitres = fuelToAdd,
                FuelType = FuelType.Diesel
            };

            // assert 
            Assert.Throws<TryFuelOverflowException>(() => tankerShipService.FuelUpTank(CORRECT_IMO,tankId,fuelUpDto));
        }

        [Theory]
        [InlineData(FuelType.HeavyFuel)]
        public void FuelUpTank_WrongFuelType_ThrowsWrongTypeFuelException( FuelType fuelType)
        {
            //arrange
            double fuelToAdd = 8001;
            int tankId = 0;

            FuelUpDto fuelUpDto = new FuelUpDto
            {
                AmountToFuelUpInLitres = fuelToAdd,
                FuelType = fuelType
            };

            // assert 
            Assert.Throws<WrongFuelTypeException>(() => tankerShipService.FuelUpTank(CORRECT_IMO, tankId, fuelUpDto));
        }


        [Theory]
        [InlineData(0)]
        public void FuelUpTank_EmptyTank_FuelAmountEqualZero(int tankId)
        {
            // act
            tankerShipService.EmptyTank(CORRECT_IMO, tankId);

            // assert
            var amountAfter = ((TankerShip)shipService.GetShipByIMO(CORRECT_IMO)).Tanks[tankId].CurrentAmountInLitres;
            Assert.Equal(0, amountAfter);
        }
    }

}
