using Business_Logic_Layer.Interfaces;
using Core.Responses;
using Data_Accces_Layer;
using Data_Accces_Layer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class GetAllLocationsService : IGetAllLocationsService
    {
        private readonly ILocationRepository _locationRepository;
        public GetAllLocationsService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }
        public async Task<RequestResult> GetAllLocationsAsync(int limit)
        {
            return await _locationRepository.GetAllLocationsAsync(limit);
        }
    }
}
