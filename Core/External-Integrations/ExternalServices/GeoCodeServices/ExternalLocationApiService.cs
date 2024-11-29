using Core.DTOs;
using Core.External_Integrations.Interfaces;
using Core.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.External_Integrations.ExternalServices
{
    public class ExternalLocationApiService : IExternalLocationApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiKeys _apiKeys;

        public ExternalLocationApiService(HttpClient httpClient, IOptions<ApiKeys> apiKeys)
        {
            _httpClient = httpClient;
            _apiKeys = apiKeys.Value;
        }

        public async Task<LocationDto?> GetCityDataAsync(string cityName)
        {
            var response = await _httpClient.GetAsync($"?q={cityName}&apiKey={_apiKeys.GeoCodeApiKey}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<GeoCodeDataResult>(jsonResponse);

            LocationDto location = new LocationDto()
            {
                Name = responseData.items[0].address.city,
                PositionLat = responseData.items[0].position.lat,
                PositionLng = responseData.items[0].position.lng,
                Country = responseData.items[0].address.countryName,
                State = responseData.items[0].address.state,
                PostalCode = responseData.items[0].address.postalCode
            };

            return location;
        }
    }
}
