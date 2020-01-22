using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class SuperAdminCreateValidator : BaseCommandValidator<SuperAdminCreateCommand>
    {
        private readonly ISuperAdminRepository _superAdminRepository;

        public SuperAdminCreateValidator(ISuperAdminRepository superAdminRepository)
        {
            _superAdminRepository = superAdminRepository;
        }

        protected override IList<ISpecification<SuperAdminCreateCommand>> GetSpecifications(SuperAdminCreateCommand command = null)
        {
            return new List<ISpecification<SuperAdminCreateCommand>>()
            {
                new SuperAdminUsernameMustBeUnique(_superAdminRepository)
            };
        }
    }
}
