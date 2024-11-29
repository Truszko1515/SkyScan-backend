using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Options
{
    public class ExternalUrls
    {

        // GeoCode platform
        public string GeoCodeBaseUrl { get; set; } = string.Empty;
        public string GeoCodeEndpoint { get; set; } = string.Empty;

        // FlightRadar24 platform
        public string FlightRadarBaseUrl { get; set; } = string.Empty;
        public string FlightRadarEndpoint { get; set; } = string.Empty;
    }
}
