using Business_Logic_Layer.Interfaces;
using Core.Responses;
using Data_Accces_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class GetLocationService : IGetLocationService
    {
        private readonly ILocationRepository _locationRepository;

        public GetLocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<RequestResult> GetLocationByNameAsync(string cityName)
        {
            if(string.IsNullOrWhiteSpace(cityName))
            {
                return RequestResult.Failure("City name cannot be empty.");
            }

            return await _locationRepository.GetLocationAsync(cityName);
        }
    }
}
