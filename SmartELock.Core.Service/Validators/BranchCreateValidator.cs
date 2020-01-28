using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class BranchCreateValidator : BaseCommandValidator<BranchCreateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;

        public BranchCreateValidator(IUserRepository userRepository, IBranchRepository branchRepository)
        {
            _userRepository = userRepository;
            _branchRepository = branchRepository;
        }

        protected override IList<ISpecification<BranchCreateCommand>> GetSpecifications(BranchCreateCommand command = null)
        {
            return new List<ISpecification<BranchCreateCommand>>()
            {
                new HasPermissionToCreateBranch(_userRepository, _branchRepository)
            };
        }
    }
}
