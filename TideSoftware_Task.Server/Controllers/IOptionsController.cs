using Core.Options;
using Data_Accces_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace TideSoftware_Task.Server.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class IOptionsController : ControllerBase
    {
        private readonly ConnectionStrings _connectionStrings;
        private readonly ApiKeys _apiKeys;

        public IOptionsController(IOptions<ConnectionStrings> connectionStrings, IOptions<ApiKeys> apiKeys)
        {
            _connectionStrings = connectionStrings.Value;
            _apiKeys = apiKeys.Value;
        }

        [HttpGet("ConnectionString")]
        public IActionResult GetConnString()
        {
            return Ok(_connectionStrings.DefaultConnection);
        }

        [HttpGet("ApiKeys")]
        public IActionResult GetApiKeys()
        {
            return Ok(_apiKeys);
        }
    }
}
