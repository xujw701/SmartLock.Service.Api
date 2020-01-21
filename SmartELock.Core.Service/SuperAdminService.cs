using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repository;
using SmartELock.Core.Domain.Service;
using SmartELock.Core.Services.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        private readonly SuperAdminCreateValidator _superAdminCreateValidator;

        public SuperAdminService(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;

            _superAdminCreateValidator = new SuperAdminCreateValidator(_superAdminRepository);
        }

        public async Task<int> CreateSuperAdmin(SuperAdminCreateCommand command)
        {
            var validationResult = await _superAdminCreateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var superAdmin = SuperAdmin.CreateFrom(command);

            return await _superAdminRepository.CreateSuperAdmin(superAdmin);
        }
    }
}
