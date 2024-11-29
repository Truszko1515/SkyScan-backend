using Core.DTOs;
using Data_Accces_Layer;
using Data_Accces_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace TideSoftware_Task.Server.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class LocationTestController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public LocationTestController(ApplicationDbContext dbContext)

        {
            _dbContext = dbContext;
        }


        [HttpPost("Add")]
        public async Task<IActionResult> AddLocation()
        {
            City cityToInsert = new City();
            cityToInsert.Name = "Warszawa";
            cityToInsert.PositionLat = 5.123;
            cityToInsert.PositionLng = 7.456;

            await _dbContext.Locations.AddAsync(cityToInsert);
            await _dbContext.SaveChangesAsync();


            return Ok(cityToInsert);
        }

        [HttpGet("GetAll")]
        public  IActionResult GetAllLocations()
        {
            return Ok(_dbContext.Locations);
        }

        [HttpDelete("Delete/{name}")]
        public async Task<IActionResult> DeleteLocation(string name)
        {
            var cityToDelete = await _dbContext.Locations.FirstOrDefaultAsync(c => c.Name == name);

            if (cityToDelete == null)
            {
                return BadRequest("Nie znaleziono miasta o podanej nazwie");
            }

            _dbContext.Locations.Remove(cityToDelete);
            await _dbContext.SaveChangesAsync();

            return Ok(cityToDelete);
        }

        [HttpPost("Update/{name}")]
        public async Task<IActionResult> UpdateLocation(string name, [FromBody] LocationDto location)
        {
            var cityToUpdate = await _dbContext.Locations.FirstOrDefaultAsync(c => c.Name == name);

            if (cityToUpdate == null)
            {
                return BadRequest("Nie znaleziono miasta o podanej nazwie");
            }

            cityToUpdate.Name = location.Name;
            cityToUpdate.PositionLat = location.PositionLat;
            cityToUpdate.PositionLng = location.PositionLng;
            
            await _dbContext.SaveChangesAsync();

            return Ok(cityToUpdate);
        }
    }
}
