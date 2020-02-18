using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly IDbRetryHandler _dbRetryHandler;

        public PropertyRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        public async Task<int> CreateProperty(Property property)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Property_Create", new
                {
                    property.CompanyId,
                    property.BranchId,
                    property.PropertyName,
                    property.Address,
                    property.Notes,
                    property.Price,
                    property.Bedrooms,
                    property.Bathrooms,
                    property.FloorArea,
                    property.LandArea
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }

        public async Task<bool> UpdateProperty(Property property)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("Property_Update", new
                {
                    property.PropertyId,
                    property.CompanyId,
                    property.BranchId,
                    property.PropertyName,
                    property.Address,
                    property.Notes,
                    property.Price,
                    property.Bedrooms,
                    property.Bathrooms,
                    property.FloorArea,
                    property.LandArea
                });
            });

            return result > 0;
        }

        public async Task<bool> EndProperty(int propertyId)
        {
            var result = await _dbRetryHandler.QueryAsync(async connection =>
            {
                return await connection.ExecuteAsync("Property_End", new
                {
                    propertyId
                });
            });

            return result > 0;
        }

        public async Task<Property> GetProperty(int propertyId)
        {
            var property = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Property_Get", new
                {
                    propertyId
                }))
                {
                    var snapshots = reader.Read<PropertySnapshot>().ToList();

                    return snapshots.Select(snapshot => Property.CreateFrom(snapshot)).FirstOrDefault();
                }
            });

            return property;
        }

        public async Task<int> CreatePropertyFeedback(PropertyFeedback propertyFeedback)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("PropertyFeedback_Create", new
                {
                    propertyFeedback.PropertyId,
                    propertyFeedback.UserId,
                    propertyFeedback.Content
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }

        public async Task<List<PropertyFeedback>> GetPropertyFeedback(int propertyId)
        {
            var propertyFeedbacks = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("PropertyFeedback_Get", new
                {
                    propertyId
                }))
                {
                    var snapshots = reader.Read<PropertyFeedbackSnapshot>().ToList();

                    return snapshots.Select(snapshot => PropertyFeedback.CreateFrom(snapshot)).ToList();
                }
            });

            return propertyFeedbacks;
        }
    }
}
