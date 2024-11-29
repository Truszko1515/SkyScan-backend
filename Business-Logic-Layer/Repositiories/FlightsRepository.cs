using Business_Logic_Layer.Interfaces;
using Core.DTOs;
using Core.External_Integrations.Interfaces;
using Core.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Repositiories
{
    public class FlightsRepository : IFlightRepository
    {
        private readonly IFlightPositionsService _flightPositionsService;
        private readonly IAirportService _airportService;
        private readonly IAirlineService _airlineService;
        public FlightsRepository(IFlightPositionsService flightPositionsService, 
                                 IAirportService airportService,
                                 IAirlineService airlineService)
        {
            _flightPositionsService = flightPositionsService;
            _airportService = airportService;
            _airlineService = airlineService;
        }

        public async Task<RequestResult<List<AirportAirlineFlightDto>>> GetFlightAndAirportDetailsAsync(double cityLat, double cityLng)
        {
            try
            {
                // Retreiving Flights info
                RequestResult<List<FlightFullDetails>> flightsResult = await _flightPositionsService.GetFlightsInCircleAreaAsync(cityLat, cityLng);

                if (!flightsResult.IsSuccess)
                    return RequestResult<List<AirportAirlineFlightDto>>.Failure("No flights detected in this area.");

                // Creating response object
                List<AirportAirlineFlightDto> result = new List<AirportAirlineFlightDto>();

                foreach (var flight in flightsResult.Data)
                {
                    AirportAirlineFlightDto data = new AirportAirlineFlightDto(flight);

                    data.originAirportName = await _airportService.GetAirportNameAsync(flight.orig_icao);
                    data.destAirportName = await _airportService.GetAirportNameAsync(flight.dest_icao);

                    data.airline = await _airlineService.GetAirlaneNameAsync(flight.operating_as);

                    result.Add(data);
                }

                if(flightsResult.IsSuccess)
                {
                    return RequestResult<List<AirportAirlineFlightDto>>.Success("Succesfull data retreival!", data: result);
                }
                else
                {
                    return RequestResult<List<AirportAirlineFlightDto>>.Failure("Unexpected error occured");
                }

            }
            catch (Exception ex)
            {
                return RequestResult<List<AirportAirlineFlightDto>>.Failure("Error occured during flights, airport and airlines retreival.");
            }         
        }
    }
}
