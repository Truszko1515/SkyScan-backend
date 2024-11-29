using Core.Responses;
using Data_Accces_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accces_Layer.Interfaces
{
    public interface ILocationRepository
    {
        Task<bool> LocationAlreadyExistsAsync(string locationName);
        Task<RequestResult> InsertLocationAsync(City cityToInsert);
        Task<RequestResult> GetLocationAsync(string CityName);
        Task<RequestResult> DeleteLocationAsync(string cityName);
        Task<RequestResult> GetAllLocationsAsync(int limit);
    }
}
