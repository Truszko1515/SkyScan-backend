using Core.DTOs;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.External_Integrations.Interfaces
{
    public interface IFlightPositionsService
    {
        Task<RequestResult<List<FlightFullDetails>>> GetFlightsInCircleAreaAsync(double cityLatitude, double cityLongtitude);
    }
}
