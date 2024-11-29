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
    public class AirportService : IAirportService
    {
        private readonly HttpClient _httpClient;
        public AirportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetAirportNameAsync(string airportIcaoCode)
        {
            //   url of this endpoint is given below
            //  https://fr24api.flightradar24.com/api/static/airports/{code}/light;

            string uri = _httpClient.BaseAddress.AbsoluteUri.Replace("%7Bcode%7D", airportIcaoCode);

            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var airportData = JsonSerializer.Deserialize<AirportDto>(jsonResponse);

            if(airportData != null && airportData.name != null)
                return airportData.name;

            return null;
        }
    }
}
