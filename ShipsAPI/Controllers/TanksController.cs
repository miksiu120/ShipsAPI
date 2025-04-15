using Microsoft.AspNetCore.Mvc;
using ShipsAPI.Models.Tanks;
using ShipsAPI.Services.Ships;

namespace ShipsAPI.Controllers
{

    [ApiController]
    [Route("api/ship/{imo}/tank")]
    public class TanksController:ControllerBase
    {
        private readonly ITankerShipService _tankerShipService;
        public TanksController (ITankerShipService tankerShipService)
        {
            _tankerShipService = tankerShipService;
        }

        [HttpPatch("{tankId}/fuel-up")]
        public IActionResult FuelUpTank
            ([FromRoute] string imo, [FromRoute] int tankId, [FromBody] FuelUpDto fuelUpDto)
        {
            _tankerShipService.FuelUpTank(imo,tankId ,fuelUpDto);

            return Ok();
        }

        [HttpPatch("{tankId}/empty")]
        public IActionResult EmptyTank([FromRoute] string imo,[FromRoute] int tankId)
        {
            _tankerShipService.EmptyTheTank(imo, tankId);
            return Ok();
        }
    }
}
