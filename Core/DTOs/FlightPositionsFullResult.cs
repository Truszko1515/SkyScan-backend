using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class FlightPositionsFullResult
    {
        public FlightFullDetails[] data { get; set; }
    }

    public class FlightFullDetails
    {
        public string fr24_id { get; set; }
        public string? flight { get; set; }
        public string? callsign { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int track { get; set; }
        public int alt { get; set; }
        public int gspeed { get; set; }
        public int vspeed { get; set; }
        public string squawk { get; set; }
        public DateTime timestamp { get; set; }
        public string source { get; set; }
        public string? hex { get; set; }
        public string? type { get; set; }
        public string? reg { get; set; }
        public string? painted_as { get; set; }
        public string? operating_as { get; set; }
        public string? orig_iata { get; set; }
        public string? orig_icao { get; set; }
        public string? dest_iata { get; set; }
        public string? dest_icao { get; set; }
        public string? eta { get; set; }
    }

}
