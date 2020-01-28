using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using SmartELock.Core.Services.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        private readonly ICommandValidator<BranchCreateCommand> _branchCreateValidator;

        public BranchService(IBranchRepository branchRepository, ICommandValidator<BranchCreateCommand> branchCreateValidator)
        {
            _branchRepository = branchRepository;

            _branchCreateValidator = branchCreateValidator;
        }

        public async Task<int> CreateBranch(BranchCreateCommand command)
        {
            var validationResult = await _branchCreateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var branch = Branch.CreateFrom(command);

            return await _branchRepository.CreateBranch(branch);
        }
    }
}
