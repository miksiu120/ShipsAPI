namespace ShipsAPI.Models.Tanks
{
    public class FuelUpDto
    {
      public required double AmountToFuelUpInLitres { get; set; }
      public required FuelType FuelType {get;set;}
    }
}
