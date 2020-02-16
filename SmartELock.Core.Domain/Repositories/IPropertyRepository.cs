using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<int> CreateProperty(Property property);
        Task<bool> UpdateProperty(Property property);
        Task<bool> EndProperty(int propertyId);
        Task<Property> GetProperty(int propertyId);
    }
}
