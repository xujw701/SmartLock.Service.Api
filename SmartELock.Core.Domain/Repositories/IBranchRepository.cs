using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IBranchRepository
    {
        Task<int> CreateBranch(Branch branch);
        Task<Branch> GetBranch(int branchId);
    }
}
