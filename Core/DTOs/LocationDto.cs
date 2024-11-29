using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class LocationDto
    {
        public LocationDto() { }

        public LocationDto(string locationName)
        {
            this.Name = locationName;
        }

        public string Name { get; set; }
        public double PositionLat { get; set; }
        public double PositionLng { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string? State { get; set; }
    }
}
