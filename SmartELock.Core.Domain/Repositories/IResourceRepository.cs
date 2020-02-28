using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Enums;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IResourceRepository
    {
        Task<int> AddPortrait(string url);
        Task<bool> UpdatePortrait(int resPortraitId, string url);
        Task<string> GetPortrait(int resPortraitId);

        Task<string> SaveBlob(byte[] bytes, FileType fileType, ResourceType resourceType);
        Task<byte[]> LoadBlob(string url, ResourceType resourceType);
        Task<bool> DeleteBlob(string url, ResourceType resourceType);
    }
}
