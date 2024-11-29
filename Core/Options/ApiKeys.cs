using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Options
{
    public class ApiKeys
    {
        public string GeoCodeApiKey { get; set; } = string.Empty;
        public string FlightRadarApiKey { get; set; } = string.Empty;
        public string FlightRadarSandboxApiKey { get; set; } = string.Empty;
    }
}
