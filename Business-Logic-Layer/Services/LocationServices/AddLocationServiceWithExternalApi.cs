using Business_Logic_Layer.Interfaces;
using Core.DTOs;
using Core.External_Integrations.Interfaces;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services.LocationServices
{
    public class AddLocationServiceWithExternalApi : IAddLocationService
    {
        private readonly IAddLocationService _decorated;
        private readonly IExternalLocationApiService _externalApiService;

        public AddLocationServiceWithExternalApi(IAddLocationService addLocationService, IExternalLocationApiService externalApiService)
        {
            _decorated = addLocationService;
            _externalApiService = externalApiService;
        }

        public async Task<RequestResult> TryInsertLocationAsync(LocationDto location)
        {
            if (!string.IsNullOrWhiteSpace(location.Name))
            {
                var cityData = await _externalApiService.GetCityDataAsync(location.Name);

                if (cityData == null)
                {
                    return RequestResult.Failure("Could not fetch additional data for the provided city.");
                }

                location.PositionLat = cityData.PositionLat;
                location.PositionLng = cityData.PositionLng;
                location.Country = cityData.Country;
                location.State = cityData.State;
                location.PostalCode = cityData.PostalCode;
            }

            // Delegating to parent service
            return await _decorated.TryInsertLocationAsync(location);
        }
    }
}
