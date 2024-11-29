using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Options
{
    public class FlightRadarApiOptions
    {
        // length of side of square which will be flights bounding box
        // if side of square is 100km -> Radius of circle inside it is 50km 
        public double SideOfSquareKm { get; set; }
        public int limit { get; set; } = 50;
    }
}
