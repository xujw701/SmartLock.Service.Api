using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IResourceRepository
    {
        Task<int> AddPortrait(string url);
        Task<bool> UpdatePortrait(int resPortraitId, string url);
        Task<string> GetPortrait(int resPortraitId);

        Task<int> AddPropertyResource(int propertyId, string url);
        Task<List<ResProperty>> GetResPropertyList(int propertyId);
        Task<string> GetResProperty(int resPropertyId);
        Task<bool> UpdateResProperty(int resPropertyId, string url);
        Task<bool> DeleteResProperty(int resPropertyId);

        Task<string> SaveBlob(byte[] bytes, FileType fileType, ResourceType resourceType);
        Task<byte[]> LoadBlob(string url, ResourceType resourceType);
        Task<bool> DeleteBlob(string url, ResourceType resourceType);
    }
}
