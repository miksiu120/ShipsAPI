using Microsoft.AspNetCore.Mvc;
using ShipsAPI.Models;
using ShipsAPI.Services.Ships;

namespace ShipsAPI.Controllers
{
    [ApiController]
    [Route("api/ship")]
    public class ShipController : ControllerBase
    {

        private readonly IShipService _shipService;
        
        public ShipController (IShipService shipService)
        {
            _shipService = shipService;
        }

        [HttpGet()]
        public IActionResult GetShips()
        {
            var ships = _shipService.GetAllShips();
            return Ok(ships);
        }

        [HttpGet("{imo}")]
        public IActionResult GetShip([FromRoute] string imo)
        {
            var ships = _shipService.GetShipByIMO(imo);
            return Ok(ships);
        }

        [HttpPost]
        public IActionResult AddNewShip(NewShipDtoBase ship)
        {
            var addedShip = _shipService.AddNewShip(ship);
            return Ok(
                new
                {
                    Message = "Ship added to registry",
                    Ship = addedShip
                } );
        }

    }
}
