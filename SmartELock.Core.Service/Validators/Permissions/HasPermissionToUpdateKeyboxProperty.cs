﻿using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Constants;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Permissions
{
    public class HasPermissionToUpdateKeyboxProperty : ISpecification<IKeyboxPropertyCreateUpdateCommand>
    {
        private readonly IKeyboxRepository _keyboxRepository;
        private readonly IUserRepository _userRepository;

        public HasPermissionToUpdateKeyboxProperty(IKeyboxRepository keyboxRepository, IUserRepository userRepository)
        {
            _keyboxRepository = keyboxRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(IKeyboxPropertyCreateUpdateCommand command)
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

                var sameCompany = operateUser.CompanyId == keybox.CompanyId;
                var sameBranch = operateUser.BranchId == keybox.BranchId;

                var commandSameCompany = operateUser.CompanyId == command.CompanyId;
                var commandSameBranch = operateUser.BranchId == command.BranchId;

                if (operateUser.UserRoleId == UserRole.User)
                {
                    var ownKeybox = keybox.UserId == operateUser.UserId;

                    return ownKeybox;
                }
                else if (operateUser.UserRoleId <= UserRole.SalesManager)
                {
                    return sameCompany && commandSameCompany
                        && sameBranch && commandSameBranch;
                }
                else if (operateUser.UserRoleId <= UserRole.GeneralManagerer)
                {
                    return sameCompany && commandSameCompany;
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

        public string ErrorMessage(IKeyboxPropertyCreateUpdateCommand obj)
        {
            return "You must have permission to operate keybox property";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.MustHasPermission;
    }
}
