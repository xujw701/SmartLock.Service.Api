using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repository;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class SuperAdminUsernameMustBeUnique : ISpecification<ISuperAdminCommand>
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        public SuperAdminUsernameMustBeUnique(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(ISuperAdminCommand command)
        {
            var superAdmin = await _superAdminRepository.GetSuperAdmin(command.Username);

            var allow = superAdmin == null;

            return allow;
        }

        public string ErrorMessage(ISuperAdminCommand obj)
        {
            return "Super Admin username must be unique";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.FieldMustUnique;
    }
}
