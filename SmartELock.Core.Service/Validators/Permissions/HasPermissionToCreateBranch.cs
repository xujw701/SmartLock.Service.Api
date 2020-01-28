using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Permissions
{
    public class HasPermissionToCreateBranch : ISpecification<IBranchCreateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;

        public HasPermissionToCreateBranch(IUserRepository userRepository, IBranchRepository branchRepository)
        {
            _userRepository = userRepository;
            _branchRepository = branchRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IBranchCreateCommand command)
        {
            // Admin has permission to create any user
            if (command.OperatedByAdmin.HasValue && command.OperatedByAdmin.Value > 0)
            {
                return true;
            }
            else if (command.OperatedBy.HasValue && command.OperatedBy.Value > 0)
            {
                var operateUser = await _userRepository.GetUser(command.OperatedBy.Value);

                var roleOk = operateUser.UserRoleId == UserRole.GeneralManagerer;
                var sameCompany = operateUser.CompanyId == command.CompanyId;

                return roleOk && sameCompany;
            }
            else
            {
                return false;
            }
        }

        public string ErrorMessage(IBranchCreateCommand obj)
        {
            return "You must have permission to create branch";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.MustHasPermission;
    }
}
