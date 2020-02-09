using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
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

        public async Task<int> CreateProperty(User user, Property property)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Property_Create", new
                {
                    user.CompanyId,
                    user.BranchId,
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
    }
}
