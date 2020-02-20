using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Permissions
{
    public class HasPermissionToCreateUser : ISpecification<IUserCreateCommand>
    {
        private readonly IUserRepository _userRepository;

        public HasPermissionToCreateUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IUserCreateCommand command)
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

                var roleOk = operateUser.UserRoleId > command.UserRoleId;
                var sameCompany = operateUser.CompanyId == command.CompanyId;
                var sameBranch = operateUser.UserRoleId < UserRole.GeneralManagerer ? operateUser.BranchId == command.BranchId : true;

                return roleOk && sameCompany && sameBranch;
            }
            else
            {
                return false;
            }
        }

        public string ErrorMessage(IUserCreateCommand obj)
        {
            return "You must have permission to create user";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.MustHasPermission;
    }
}
