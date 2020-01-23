using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchService(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<int> CreateBranch(BranchCreateCommand command)
        {
            var branch = Branch.CreateFrom(command);

            return await _branchRepository.CreateBranch(branch);
        }
    }
}
