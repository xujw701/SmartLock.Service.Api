using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Validators;
using SmartELock.Core.Services.Validators.Specifications;
using System.Collections.Generic;

namespace SmartELock.Core.Services.Validators
{
    public class CompanyCreateValidator : BaseCommandValidator<CompanyCreateCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyCreateValidator(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        protected override IList<ISpecification<CompanyCreateCommand>> GetSpecifications(CompanyCreateCommand command = null)
        {
            return new List<ISpecification<CompanyCreateCommand>>()
            {
                new CompanyNameMustBeUnique(_companyRepository)
            };
        }
    }
}
