using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TideSoftware_Task.Server.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class DbHealthController : ControllerBase
    {
        private readonly IDatabaseHealthCheckService _healthCheckService;
        public DbHealthController(IDatabaseHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet("database")]
        public async Task<IActionResult> CheckDatabaseConnection()
        {
            if (await _healthCheckService.CanConnectAsync())
            {
                return Ok(new { status = "Healthy" });
            }
            else
            {
                return StatusCode(503, new { status = "Unhealthy" });
            }
        }
    }
}
