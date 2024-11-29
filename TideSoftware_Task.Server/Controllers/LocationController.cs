using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Core.DTOs;
using Data_Accces_Layer.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TideSoftware_Task.Server.Controllers
{
    [Route("api/[controller]/")]
    [EnableCors("LocalHostPolicy")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IAddLocationService _addLocationService;
        private readonly IGetLocationService _getLocationServce;
        private readonly IGetAllLocationsService _getAllLocationsService;
        private readonly IDeleteLocationService _deleteLocationService;
        public LocationController(IAddLocationService addLocationService,
                                  IGetLocationService getLocationServce,
                                  IGetAllLocationsService getAllLocationsService,
                                  IDeleteLocationService deleteLocationService)
        {
            _addLocationService = addLocationService;
            _getLocationServce = getLocationServce;
            _getAllLocationsService = getAllLocationsService;
            _deleteLocationService = deleteLocationService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLocation([FromBody] string locationName)
        {
            var result = await _addLocationService.TryInsertLocationAsync(new LocationDto(locationName));

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message); 
        }

        [HttpGet("GetAll/{limit?}")]
        public async Task<IActionResult> GetAllLocation(int limit = 30)
        {
            var result = await _getAllLocationsService.GetAllLocationsAsync(limit);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteLocation([FromBody] string locationName)
        {
            var result = await _deleteLocationService.DeleteLocationAsync(locationName);

            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("{cityName}")]
        public async Task<IActionResult> GetLocation(string cityName)
        {
            var result = await _getLocationServce.GetLocationByNameAsync(cityName);

            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
