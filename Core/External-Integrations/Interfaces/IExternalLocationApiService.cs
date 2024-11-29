using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.External_Integrations.Interfaces
{
    public interface IExternalLocationApiService
    {
        Task<LocationDto?> GetCityDataAsync(string cityName);
    }
}
