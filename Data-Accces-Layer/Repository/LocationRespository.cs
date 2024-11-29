using Core.DTOs;
using Core.Responses;
using Data_Accces_Layer.Interfaces;
using Data_Accces_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Accces_Layer.Repository
{
    public class LocationRespository : ILocationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LocationRespository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> LocationAlreadyExistsAsync(string locationName)
        {
            var result = await _dbContext.Locations.FirstOrDefaultAsync(c => c.Name == locationName);

            if (result != null)
                return true;

            return false;
        }

        public async Task<RequestResult> InsertLocationAsync(City cityToInsert)
        {
            try
            {
                await _dbContext.Locations.AddAsync(cityToInsert);
                await _dbContext.SaveChangesAsync();
                return RequestResult.Success("Location added successfully.");
            }
            catch (Exception ex)
            {
                return RequestResult.Failure($"An error occurred while inserting the location: {ex.Message}");
            }
        }

        public async Task<RequestResult> GetLocationAsync(string cityName)
        {
            try
            {
                var city = await _dbContext.Locations.FirstOrDefaultAsync(c => c.Name == cityName);

                if(city != null)
                    return RequestResult.Success(data: city);

                return RequestResult.Failure("City does not exist");
            }
            catch (Exception ex)
            {
                return RequestResult.Failure($"An error occurred while retreiving the location: {ex.Message}");
            }

        }

        public async Task<RequestResult> DeleteLocationAsync(string cityName)
        {
            try
            {
                var cityToRemove = await _dbContext.Locations.FirstOrDefaultAsync(c => c.Name == cityName);

                if (cityToRemove == null)
                    return RequestResult.Failure("City with given name does not exist.");

                _dbContext.Locations.Remove(cityToRemove);
                await _dbContext.SaveChangesAsync();

                return RequestResult.Success("City removed correctly.", cityToRemove);
            }
            catch (Exception ex) 
            {
                return RequestResult.Failure($"An error occurred while retreiving locations: {ex.Message}");
            }
        }

        public async Task<RequestResult> GetAllLocationsAsync(int limit)
        {
            try
            {
                if(limit > 20) limit = 20;
                IQueryable<City> cities = _dbContext.Locations.Take(limit);
                return RequestResult.Success("Pomyślnie odebrano miasta", await cities.ToListAsync());
            }
            catch (Exception ex)
            {
                return RequestResult.Failure($"An error occurred while retreiving locations: {ex.Message}");
            }
        }
    }
}
