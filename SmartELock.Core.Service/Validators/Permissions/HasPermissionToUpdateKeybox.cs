using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Permissions
{
    public class HasPermissionToUpdateKeybox : ISpecification<IKeyboxUpdateCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public HasPermissionToUpdateKeybox(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxUpdateCommand command)
        {
            // Admin has permission to create any user
            if (command.OperatedByAdmin.HasValue && command.OperatedByAdmin.Value > 0)
            {
                return true;
            }
            else if (command.OperatedBy.HasValue && command.OperatedBy.Value > 0)
            {
                var keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);
                var keyboxOwner = await _userRepository.GetUser(command.UserId);
                var operateUser = await _userRepository.GetUser(command.OperatedBy.Value);

                if (keybox == null || operateUser == null) return false;

                var keyboxSameCompany = operateUser.CompanyId == keybox.CompanyId;
                var keyboxSameBranch = operateUser.BranchId == keybox.BranchId;

                var keyboxOwnerSameCompany = keyboxOwner.CompanyId == keybox.CompanyId;
                var keyboxOwnerSameBranch = keyboxOwner.BranchId == keybox.BranchId;

                if (operateUser.UserRoleId >= UserRole.BranchManager)
                {
                    return keyboxSameCompany && keyboxOwnerSameCompany;
                }
                else if (operateUser.UserRoleId >= UserRole.UserAdmin)
                {
                    return keyboxSameCompany && keyboxSameBranch
                        && keyboxOwnerSameCompany && keyboxOwnerSameBranch;
                }
                else
                {
                    return keybox.UserId == operateUser.UserId
                        && keybox.BranchId == command.BranchId
                        && keybox.UserId == command.UserId;
                }
            }
            else
            {
                return false;
            }
        }

        public string ErrorMessage(IKeyboxUpdateCommand obj)
        {
            return "You must have permission to update keybox";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.MustHasPermission;
    }
}
