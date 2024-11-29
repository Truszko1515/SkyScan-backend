using Core.DTOs;
using Core.External_Integrations.Interfaces;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.External_Integrations.ExternalServices.FlightRadarServices
{
    public class FlightPositionsSandboxService : IFlightPositionsService
    {
        private readonly HttpClient _httpClient;
        public FlightPositionsSandboxService(HttpClient httpClient) 
        { 
            _httpClient = httpClient;
        }
        public async Task<RequestResult<List<FlightFullDetails>>> GetFlightsInCircleAreaAsync(double cityLatitude, double cityLongtitude)
        {
            double north = 42.473;
            double south = 37.331;
            double west = -10.014;
            double east = -4.115;

            string v1 = north.ToString().Replace(",",".");
            string v2 = south.ToString().Replace(",", ".");
            string v3 = west.ToString().Replace(",", ".");
            string v4 = east.ToString().Replace(",", ".");

            var response = await _httpClient.GetAsync($"?bounds={v1}%2C{v2}%2C{v3}%2C{v4}&limit=5");

            if (!response.IsSuccessStatusCode)
            {
                return RequestResult<List<FlightFullDetails>>.Failure("Error occured during flights retreival");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var flights = JsonSerializer.Deserialize<FlightPositionsLightResult>(jsonResponse);


            return RequestResult<List<FlightFullDetails>>.Success($"Succesfull {flights.data.Length} flights retreival!"); ;
        }
    }
}
