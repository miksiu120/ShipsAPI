using ShipsAPI.Models.Ships;

namespace ShipsAPI.Repositories
{

    public interface IShipRepository
    {


        List<Ship> GetAllShips();
        Ship? GetShipByIMO(string imo);
        void InsertNewShip(Ship ship);
        void UpdateShip(Ship ship);
        void DeleteShipByIMO(string IMO);
    }

    public class ShipRepository:IShipRepository
    {
        private readonly List<Ship> _ships;
        public ShipRepository()
        {
            _ships = new List<Ship>();
        }

        public List<Ship> GetAllShips()
        {
            return _ships;
        }

        public Ship? GetShipByIMO(string imo)
        {
            return _ships.Find(s => s.IMO == imo);
        }

        public void InsertNewShip(Ship ship)
        {
            _ships.Add(ship);
        }

        public void UpdateShip(Ship ship)
        {
            var index = _ships.FindIndex(s => s.IMO == ship.IMO);
            if (index == -1)
            {
                throw new InvalidOperationException("Ship not found for update.");
            }

            _ships[index] = ship;  
        }

        public void DeleteShipByIMO(string IMO)
        {
            var index = _ships.FindIndex(s => s.IMO == IMO);

            if (index == -1)
            {
                throw new InvalidOperationException("Ship not found for deletion.");
            }

            _ships.RemoveAt(index);
        }

    }
}
