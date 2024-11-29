using Core.DTOs;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Interfaces
{
    public interface IFlightRepository 
    {
        Task<RequestResult<List<AirportAirlineFlightDto>>> GetFlightAndAirportDetailsAsync(double cityLat, double cityLng);
    }
}
