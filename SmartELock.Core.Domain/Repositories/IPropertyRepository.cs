using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IPropertyRepository
    {
        Task<int> CreateProperty(User user, Property property);
    }
}
