using ShipsAPI.Models.Tanks;

namespace ShipsAPI.Exceptions
{
    public class ShipNotFoundException : Exception
    {
        public ShipNotFoundException(string imo)
            : base($"Ship with IMO {imo} not found.")
        {
        }
    }

    public class PassengerNotFoundException : Exception
    {
        public PassengerNotFoundException(int id)
            : base($"Passenger with id {id} not found")
        {

        }
    }

    public class InvalidPassengerException : Exception
    {
        public InvalidPassengerException(int passengerId)
            : base($"Passenger with ID {passengerId} is invalid or already on board.")
        {
        }
    }

    public class TankNotFoundException : Exception
    {
        public TankNotFoundException(int tankId)
            : base($"Tank with ID {tankId} not found.")
        {
        }
    }

    public class InvalidShipOperationException : Exception
    {
        public InvalidShipOperationException(string message)
            : base(message)
        {
        }
    }


    public class TryFuelOverflowException : Exception
    {
        public TryFuelOverflowException(double overflowAmount)
        : base($"Attempted to overfill the tank by {overflowAmount:F2} liters.")
        {
            
        }
    }

    public class WrongFuelTypeException : Exception
    {
        public WrongFuelTypeException(FuelType fuelType)
        : base($"Attempted to fill the tank by wrong fuel type ({fuelType.ToString()})")
        {

        }
    }

}
