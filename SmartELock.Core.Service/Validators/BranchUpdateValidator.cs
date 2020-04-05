using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Permissions;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class BranchUpdateValidator : BaseCommandValidator<BranchUpdateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;

        public BranchUpdateValidator(IUserRepository userRepository, IBranchRepository branchRepository)
        {
            _userRepository = userRepository;
            _branchRepository = branchRepository;
        }

        protected override IList<ISpecification<BranchUpdateCommand>> GetSpecifications(BranchUpdateCommand command = null)
        {
            return new List<ISpecification<BranchUpdateCommand>>()
            {
                new HasPermissionToUpdateBranch(_userRepository, _branchRepository)
            };
        }
    }
}
