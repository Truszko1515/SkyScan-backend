using Core.DTOs;
using Core.External_Integrations.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.External_Integrations.ExternalServices.FlightRadarServices
{
    public class AirlineService : IAirlineService
    {
        private readonly HttpClient _httpClient;
        public AirlineService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> GetAirlaneNameAsync(string airlineIcaoCode)
        {
            //   url of this endpoint is given below
            //  https://fr24api.flightradar24.com/api/static/airlines/{icao}/light;

            string uri = _httpClient.BaseAddress.AbsoluteUri.Replace("%7Bicao%7D", airlineIcaoCode);

            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var airlinetData = JsonSerializer.Deserialize<AirlineDto>(jsonResponse);

            if (airlinetData != null && airlinetData.name != null)
                return airlinetData.name;

            return null;
        }
    }
}
