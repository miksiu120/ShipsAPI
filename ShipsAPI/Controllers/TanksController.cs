using Microsoft.AspNetCore.Mvc;
using ShipsAPI.Models.Passengers;
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

            return Ok(new
            {
                Message = $"Ship with imo:{imo} fueld up a tank with id:{tankId}"
            });
        }

        [HttpPatch("{tankId}/empty")]
        public IActionResult EmptyTank([FromRoute] string imo,[FromRoute] int tankId)
        {
            _tankerShipService.EmptyTank(imo, tankId);
            return Ok(new
            {
                Message = $"Ship with imo:{imo} empited tank with id:{tankId}"
            });
        }
    }
}
