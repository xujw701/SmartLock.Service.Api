using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Permissions
{
    public class HasPermissionToDeleteKeyboxProperty : ISpecification<KeyboxPropertyDeleteCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public HasPermissionToDeleteKeyboxProperty(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(KeyboxPropertyDeleteCommand command)
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

                var ownKeybox = keybox.UserId == operateUser.UserId;
                var sameCompany = operateUser.CompanyId == keybox.CompanyId;
                var sameBranch = operateUser.BranchId == keybox.BranchId;

                return ownKeybox && sameCompany && sameBranch;
            }
            else
            {
                return false;
            }
        }

        public string ErrorMessage(KeyboxPropertyDeleteCommand obj)
        {
            return "You must have permission to delete keybox property";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.MustHasPermission;
    }
}
