using Business_Logic_Layer.Interfaces;
using Core.DTOs;
using Core.External_Integrations.Interfaces;
using Core.Helpers;
using Data_Accces_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TideSoftware_Task.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightPositionsService _flightPositionsService;
        private readonly IFlightRepository _flightRepository;
        public FlightsController(IFlightPositionsService flightPositionsService, IFlightRepository flightRepository)
        {
            _flightPositionsService = flightPositionsService;
            _flightRepository = flightRepository;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost("Positions/ByCity")]
        public async Task<IActionResult> GetFlightsInfoInCityBoundingBox([FromBody] CityPositionDto city)
        {

            var result = await _flightPositionsService.GetFlightsInCircleAreaAsync(city.CityLatitude, city.CityLongitude);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }

        [HttpGet("Info/InCityRadius/{cityLatitude},{cityLongtitude}")]
        public async Task<IActionResult> GetFlightsFullData(double cityLatitude, double cityLongtitude)
        {
            var result = await _flightRepository.GetFlightAndAirportDetailsAsync(cityLatitude, cityLongtitude);

            if(!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Data);
        }
    }
}
