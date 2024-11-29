using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public class CityBoundsHelper
    {
            public static BoundingBoxDto CalculateBoundingBox(double cityLatitude, double cityLongitude, double SideOfSqareKm)
            {
                double halfSideofSquareKm = SideOfSqareKm / 2;

                // shift in degrees
                double diffrenceLat = halfSideofSquareKm / 111.32;
                double diffrenceLng = halfSideofSquareKm / (111.32 * Math.Cos(DegreesToRadians(cityLatitude)));

                // bounding box coordinates
                BoundingBoxDto boundingBox = new BoundingBoxDto()
                {
                    north = Math.Round((cityLatitude + diffrenceLat), 3).ToString().Replace("," , "."),
                    south = Math.Round((cityLatitude - diffrenceLat), 3).ToString().Replace(",", "."),
                    west = Math.Round((cityLongitude - diffrenceLng), 3).ToString().Replace(",", "."),
                    east = Math.Round((cityLongitude + diffrenceLng), 3).ToString().Replace(",", ".")
                };

                return boundingBox;
            }

            private static double DegreesToRadians(double degrees)
            {
                return degrees * Math.PI / 180.0;
            }
    }
}
