using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Permissions
{
    public class HasPermissionToAssignKeybox : ISpecification<IKeyboxCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public HasPermissionToAssignKeybox(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxCommand command)
        {
            // Admin has permission to create any user
            if (command.OperatedByAdmin.HasValue && command.OperatedByAdmin.Value > 0)
            {
                return true;
            }
            else if (command.OperatedBy.HasValue && command.OperatedBy.Value > 0)
            {
                var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);
                var operateUser = await _userRepository.GetUser(command.OperatedBy.Value);

                if (keybox == null || operateUser == null) return false;

                var roleOk = operateUser.UserRoleId == UserRole.GeneralManagerer;
                var sameCompany = operateUser.CompanyId == keybox.CompanyId;

                return roleOk && sameCompany;
            }
            else
            {
                return false;
            }
        }

        public string ErrorMessage(IKeyboxCommand obj)
        {
            return "You must have permission to assign keybox";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.MustHasPermission;
    }
}
