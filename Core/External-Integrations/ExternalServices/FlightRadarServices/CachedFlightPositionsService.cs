using Core.DTOs;
using Core.External_Integrations.Interfaces;
using Core.Responses;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.External_Integrations.ExternalServices.FlightRadarServices
{
    public class CachedFlightPositionsService : IFlightPositionsService
    {
        private readonly IFlightPositionsService _decorated;
        private readonly IMemoryCache _cache;

        public CachedFlightPositionsService(IFlightPositionsService flightPositionsService, IMemoryCache cache)
        {
            _decorated = flightPositionsService;
            _cache = cache;
        }

        public async Task<RequestResult<List<FlightFullDetails>>> GetFlightsInCircleAreaAsync(double cityLatitude, double cityLongtitude)
        {
            string cacheKey = $"Flights_{cityLatitude}_{cityLongtitude}";

            if (_cache.TryGetValue(cacheKey, out List<FlightFullDetails> cachedFlights))
            {
                return RequestResult<List<FlightFullDetails>>.Success("Flights retrieved from cache.", cachedFlights);
            }

            var result = await _decorated.GetFlightsInCircleAreaAsync(cityLatitude, cityLongtitude);

            // Zapisz wynik do cache, jeśli operacja się powiodła
            if (result.IsSuccess && result.Data != null)
            {
                _cache.Set(cacheKey, result.Data, TimeSpan.FromMinutes(5)); 
            }

            return result;
        }
    }
}
