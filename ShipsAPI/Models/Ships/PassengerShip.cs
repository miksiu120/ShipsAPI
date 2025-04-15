using ShipsAPI.Exceptions;
using ShipsAPI.Models.Passengers;

namespace ShipsAPI.Models.Ships
{
    public class PassengerShip : Ship
    {
        public int PeopleCounter { get; set; } = 0;
        public List<Passenger> Passengers { get; set; } = new List<Passenger>();

        public Passenger AddNewPassenger(PassengerDto passengerToAdd)
        {
            var newPassenger = new Passenger
            {
                Name = passengerToAdd.Name,
                Surname = passengerToAdd.Surname,
                Id = PeopleCounter++
            };

            Passengers.Add(newPassenger);

            return newPassenger;
        }

        public IEnumerable<Passenger> AddNewPassengers(IEnumerable<PassengerDto> passengersToAdd)
        {
            var newPassengers = new List<Passenger>();

            foreach(var passenger in passengersToAdd)
            {
                var newPassenger = new Passenger
                {
                    Name = passenger.Name,
                    Surname = passenger.Surname,
                    Id = PeopleCounter++
                };

                newPassengers.Add(newPassenger);
            }

            Passengers.AddRange(newPassengers);
            return Passengers;
        }


        public void EditPassenger(int id, PassengerDto passengerData)
        {
            var passengerIndex = Passengers.FindIndex(p => p.Id == id);

            if (passengerIndex == -1)
            {
                throw new InvalidPassengerException(id);
            }
            Passengers[passengerIndex].Name = passengerData.Name;
            Passengers[passengerIndex].Surname = passengerData.Surname;
        }

        public void RemovePassenger(int id)
        {
            var passenger = Passengers.FirstOrDefault(p => p.Id == id);

            if (passenger is null)
            {
                throw new InvalidPassengerException(id);
            }

            Passengers.Remove(passenger);
        }

        public Passenger GetPassenger(int id)
        {
            var passenger = Passengers.FirstOrDefault(p => p.Id == id);

            if (passenger is null)
            {
                throw new PassengerNotFoundException(id);
            }

            return passenger;
        }
    }
}
