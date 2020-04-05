using SmartELock.Core.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IBranchRepository
    {
        Task<int> CreateBranch(Branch branch);
        Task<Branch> GetBranch(int branchId);
        Task<List<Branch>> GetBranchesByUserId(int userId);
        Task<bool> UpdateBranch(Branch branch);
    }
}
