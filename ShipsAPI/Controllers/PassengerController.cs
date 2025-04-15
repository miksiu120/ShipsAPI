using Microsoft.AspNetCore.Mvc;
using ShipsAPI.Models.Passengers;
using ShipsAPI.Services.Ships;
using System.Security.Cryptography.X509Certificates;

namespace ShipsAPI.Controllers
{

    [ApiController]
    [Route("api/ship/{imo}/passenger")]
    public class PassengerController : ControllerBase
    {
        private readonly IPassengerShipService _passengerService;

        public PassengerController(IPassengerShipService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpPost()]
        public IActionResult AddNewPassenger([FromRoute] string imo, [FromBody] PassengerDto passenger)
        {
            var newPassenger = _passengerService.AddNewPassenger(imo, passenger);

            return Ok(new
            {
                Message = $"{passenger.Name} {passenger.Surname} added to ship with IMO:{imo}",
                Passenger = newPassenger
            });
        }

        [HttpPost("many")]
        public IActionResult AddNewPassengers([FromRoute] string imo, [FromBody] IEnumerable<PassengerDto> passengers)
        {
            var newPassengers = _passengerService.AddNewPassengers(imo, passengers);

            return Ok(new
            {
                Message = $"Passengers added to ship with IMO:{imo}",
                Passengers = newPassengers
            });
        }

        [HttpDelete("{passengerId}")]
        public IActionResult DeletePassenger([FromRoute] string imo, [FromRoute] int passengerId)
        {
            _passengerService.DeletePassenger(imo, passengerId);
            return Ok();
        }

        [HttpPatch("{passengerId}")]
        public IActionResult UpdatePassenger
            ([FromRoute] string imo, [FromRoute] int passengerId, [FromBody] PassengerDto passengerDto)
        {
            _passengerService.UpdatePassenger(imo, passengerId, passengerDto);
            return Ok();
        }

        [HttpGet("{passengerId}")]
        public IActionResult GetPassenger([FromRoute] string imo, [FromRoute] int passengerId)
        {
            var passenger = _passengerService.GetPassenger(imo, passengerId);

            return Ok(passenger);
        }

        [HttpGet()]
        public IActionResult GetPassengers([FromRoute] string imo)
        {
            var passenger = _passengerService.GetPassengers(imo);

            return Ok(passenger);
        }

    }
}
