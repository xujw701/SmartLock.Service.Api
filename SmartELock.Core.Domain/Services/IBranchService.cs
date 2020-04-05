using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IBranchService
    {
        Task<int> CreateBranch(BranchCreateCommand command);
        Task<bool> UpdateBranch(BranchUpdateCommand command);
        Task<List<Branch>> GetBranches(User currentUser);
    }
}
