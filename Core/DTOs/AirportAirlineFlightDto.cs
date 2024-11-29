using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class AirportAirlineFlightDto
    {
        public AirportAirlineFlightDto(FlightFullDetails f)
        {
            this.flight = f.flight;
            this.lat = f.lat;
            this.lon = f.lon;
            this.alt = f.alt;
            this.eta = f.eta;
        }

        public string? flight { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int alt { get; set; }
        public string? airline { get; set; }
        public string? originAirportName { get; set; }
        public string? destAirportName { get; set; }
        
        // Estimated time arrival
        public string? eta { get; set; }
    }
}
