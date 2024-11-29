using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public static class FlightRadarHelper
    {
        private const double EarthRadiusKm = 6371.0;

        // Function that calculates city and circle radius area (uses Haversine formula)
        public static double HaversineDistance(double cityCenterLat, double cityCenterLng, double airPlaneLat, double airPlaneLng)
        {
            double diffrenceLat = DegreesToRadians(airPlaneLat - cityCenterLat);
            double diffrenceLng = DegreesToRadians(airPlaneLng - cityCenterLng);

            double cityCenterLatRad = DegreesToRadians(cityCenterLat);
            double airPlaneLatRad = DegreesToRadians(airPlaneLat);

            double a = Math.Sin(diffrenceLat / 2) * Math.Sin(diffrenceLat / 2) +
                       Math.Cos(cityCenterLatRad) * Math.Cos(airPlaneLatRad) *
                       Math.Sin(diffrenceLng / 2) * Math.Sin(diffrenceLng / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        // Function that checks if city is inside circle area of given radius
        public static bool IsAirPlaneInCityRange(double cityCenterLat, double cityCenterLng, double cityRadiusKm, double airPlaneLat, double airPlaneLng)
        {
            double distance = HaversineDistance(cityCenterLat, cityCenterLng, airPlaneLat, airPlaneLng);
            return distance <= cityRadiusKm;
        }


        // Converting degrees into radians
        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
