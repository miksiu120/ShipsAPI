
using ShipsAPI.Exceptions;
using ShipsAPI.Models.Tanks;

namespace ShipsAPI.Models.Ships
{
    public class TankerShip : Ship
    {
        public List<Tank> Tanks { get; set; } = new List<Tank>();

        public void fuelUpTanker(int tankId, FuelUpDto fuelUpDto)
        {

            Tank? tank = Tanks.Where(t => t.Id == tankId).FirstOrDefault();

            if (tank == null)
            {
                throw new TankNotFoundException(tankId);
            }

            tank.FuelUp(fuelUpDto);
        }

        public void emptyTanker(int tankId)
        {
            Tank? tank = Tanks.Where(t => t.Id == tankId).FirstOrDefault();

            if (tank == null)
            {
                throw new TankNotFoundException(tankId);
            }

            tank.CurrentAmountInLitres = 0;
        }
    }

}

