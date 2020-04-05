using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Permissions
{
    public class HasPermissionToUpdateBranch : ISpecification<IBranchUpdateCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;

        public HasPermissionToUpdateBranch(IUserRepository userRepository, IBranchRepository branchRepository)
        {
            _userRepository = userRepository;
            _branchRepository = branchRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IBranchUpdateCommand command)
        {
            // Admin has permission to create any user
            if (command.OperatedByAdmin.HasValue && command.OperatedByAdmin.Value > 0)
            {
                return true;
            }
            else if (command.OperatedBy.HasValue && command.OperatedBy.Value > 0)
            {
                var operateUser = await _userRepository.GetUser(command.OperatedBy.Value);

                if (operateUser == null) return false;

                var sameCompany = operateUser.CompanyId == command.CompanyId;

                if (operateUser.UserRoleId == UserRole.BranchManager)
                {
                    var sameBranch = operateUser.BranchId == command.BranchId;

                    return sameCompany && sameBranch;
                }
                else if (operateUser.UserRoleId > UserRole.BranchManager)
                {
                    return sameCompany;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public string ErrorMessage(IBranchUpdateCommand obj)
        {
            return "You must have permission to update branch";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.MustHasPermission;
    }
}
