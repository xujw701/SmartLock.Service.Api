using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Permissions
{
    public class HasPermissionToAccessKeybox : ISpecification<IKeyboxCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public HasPermissionToAccessKeybox(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
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
                Keybox keybox = null;
                if (command.KeyboxId > 0)
                {
                    keybox = await _keyboxRepository.GetKeybox(command.KeyboxId);
                }
                else
                {
                    keybox = await _keyboxRepository.GetKeyboxByUuid(command.Uuid);
                }
                
                var operateUser = await _userRepository.GetUser(command.OperatedBy.Value);

                var sameCompany = operateUser.CompanyId == keybox.CompanyId;

                return sameCompany;
            }
            else
            {
                return false;
            }
        }

        public string ErrorMessage(IKeyboxCommand obj)
        {
            return "You must have permission to access keybox";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.MustHasPermission;
    }
}
