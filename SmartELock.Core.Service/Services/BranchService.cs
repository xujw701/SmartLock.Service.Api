using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using SmartELock.Core.Services.Validators;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        private readonly ICommandValidator<BranchCreateCommand> _branchCreateValidator;
        private readonly ICommandValidator<BranchUpdateCommand> _branchUpdateValidator;

        public BranchService(IBranchRepository branchRepository, ICommandValidator<BranchCreateCommand> branchCreateValidator, ICommandValidator<BranchUpdateCommand> branchUpdateValidator)
        {
            _branchRepository = branchRepository;

            _branchCreateValidator = branchCreateValidator;
            _branchUpdateValidator = branchUpdateValidator;
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

        public async Task<bool> UpdateBranch(BranchUpdateCommand command)
        {
            var validationResult = await _branchUpdateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var branch = Branch.CreateFrom(command);

            return await _branchRepository.UpdateBranch(branch);
        }

        public async Task<List<Branch>> GetBranches(User currentUser)
        {
            var allBranches = await _branchRepository.GetBranchesByUserId(currentUser.UserId);

            if (currentUser.UserRoleId >= UserRole.BranchManager)
            {
                return allBranches;
            }
            else
            {
                return allBranches.Where(b => b.BranchId == currentUser.BranchId).ToList();
            }
        }
    }
}
