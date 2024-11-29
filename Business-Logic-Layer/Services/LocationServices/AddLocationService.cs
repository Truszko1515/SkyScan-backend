using Business_Logic_Layer.Interfaces;
using Core.DTOs;
using Core.Responses;
using Data_Accces_Layer;
using Data_Accces_Layer.Interfaces;
using Data_Accces_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Services
{
    public class AddLocationService : IAddLocationService
    {
        private readonly ILocationRepository _locationRepository;
        public AddLocationService(ILocationRepository locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<RequestResult> TryInsertLocationAsync(LocationDto location) 
        {
            if (string.IsNullOrWhiteSpace(location.Name))
            {
                return RequestResult.Failure("Location name cannot be empty.");
            }

            bool locationExists = await _locationRepository.LocationAlreadyExistsAsync(location.Name);

            if (locationExists)
            {
                return RequestResult.Failure("Location already exists.");
            }

            City cityToInsert = new City()
            {
                Name = location.Name,
                PositionLat = location.PositionLat,
                PositionLng = location.PositionLng,
                Country = location.Country,
                State = location.State,
                PostalCode = location.PostalCode
            };

            var insertResult = await _locationRepository.InsertLocationAsync(cityToInsert);

                if (insertResult.IsSuccess)
                {
                    return RequestResult.Success(insertResult.Message, cityToInsert);
                }

                return RequestResult.Failure(insertResult.Message);

        }
    }
}
