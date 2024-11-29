using Core.DTOs;
using Core.External_Integrations.Interfaces;
using Core.Helpers;
using Core.Options;
using Core.Responses;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.External_Integrations.ExternalServices.FlightRadarServices
{
    public class FlightPositionsService : IFlightPositionsService
    {
        private readonly HttpClient _httpClient;
        private readonly FlightRadarApiOptions _options;
        private readonly IMemoryCache _cache;
        public FlightPositionsService(HttpClient httpClient, IOptions<FlightRadarApiOptions> options, IMemoryCache memoryCache)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _cache = memoryCache;
        }

        public async Task<RequestResult<List<FlightFullDetails>>> GetFlightsInCircleAreaAsync(double cityLatitude, double cityLongtitude)
        {
            BoundingBoxDto bounds = CityBoundsHelper.CalculateBoundingBox(cityLatitude, cityLongtitude, _options.SideOfSquareKm);

            var response = await _httpClient.GetAsync(
                $"?bounds={bounds.north}%2C{bounds.south}%2C{bounds.west}%2C{bounds.east}&limit={_options.limit}&categories=P,C");

            if (!response.IsSuccessStatusCode)
            {
                return RequestResult<List<FlightFullDetails>>.Failure("Error occured during retreiving flights.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var FlightsInBoundingBox = JsonSerializer.Deserialize<FlightPositionsFullResult>(jsonResponse);

            RequestResult<List<FlightFullDetails>> result = RetreiveFlightsOnlyInArea(FlightsInBoundingBox, cityLatitude, cityLongtitude);

            return result;
        }

        private RequestResult<List<FlightFullDetails>> RetreiveFlightsOnlyInArea(FlightPositionsFullResult FlightsInBoundingBox, double cityCenterLat, double cityCenterLng)
        {
            if(FlightsInBoundingBox == null)
            {
                return RequestResult<List<FlightFullDetails>>.Failure("Error Occured during flights retreival");
            }

            if (FlightsInBoundingBox.data.Length < 1 && FlightsInBoundingBox != null)
            {
                return RequestResult<List<FlightFullDetails>>.Success($"No flights detected in {_options.SideOfSquareKm/2}km radius of City");
            }

            List<FlightFullDetails> flightsInCircleRadius = new List<FlightFullDetails>();

            foreach (var flight in FlightsInBoundingBox.data)
            {
                if(FlightRadarHelper.IsAirPlaneInCityRange(cityCenterLat, cityCenterLng, _options.SideOfSquareKm/2,
                                                           flight.lat, flight.lon))
                {
                    flightsInCircleRadius.Add(flight);
                }
            }

            return RequestResult<List<FlightFullDetails>>.Success($"Succesfull flights retreival in {_options.SideOfSquareKm/2}km radius of City!", data: flightsInCircleRadius);

        }
    }
}
