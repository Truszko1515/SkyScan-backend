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
    public class DeleteLocationService : IDeleteLocationService
    {
        private readonly ILocationRepository _locationRespository;
        public DeleteLocationService(ILocationRepository locationRepository)
        {
            _locationRespository = locationRepository;
        }

        public async Task<RequestResult> DeleteLocationAsync(string cityName)
        {
           var result = await _locationRespository.DeleteLocationAsync(cityName);

            if (result.IsSuccess)
                return RequestResult.Success(result.Message, result.Data);

            return RequestResult.Failure(result.Message);
        }
    }
}
