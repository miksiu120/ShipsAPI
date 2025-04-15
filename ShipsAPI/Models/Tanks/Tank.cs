using ShipsAPI.Exceptions;

namespace ShipsAPI.Models.Tanks
{
    public class Tank
    {
        public int Id { get; set; }
        public double CapacityInLitres { get; set; }
        public FuelType? ActualFuelType { get; set; }
        public double CurrentAmountInLitres { get; set; }

        
        public void FuelUp(FuelUpDto fuelToAdd)
        {

            if(CurrentAmountInLitres > 0 && fuelToAdd.FuelType != ActualFuelType)
            {
                throw new WrongFuelTypeException(fuelToAdd.FuelType);
            }

            if(CurrentAmountInLitres == 0 && ActualFuelType != fuelToAdd.FuelType)
            {
                ActualFuelType = fuelToAdd.FuelType;
            }

            if(CapacityInLitres < fuelToAdd.AmountToFuelUpInLitres + CurrentAmountInLitres)
            {
                var overflowAmount = CurrentAmountInLitres + fuelToAdd.AmountToFuelUpInLitres - CapacityInLitres;
                throw new TryFuelOverflowException(overflowAmount);
            }

            CurrentAmountInLitres += fuelToAdd.AmountToFuelUpInLitres;
        }
    }
}
