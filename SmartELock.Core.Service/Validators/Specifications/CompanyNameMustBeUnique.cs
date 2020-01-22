using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repository;
using SmartELock.Core.Domain.Validators;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Validators.Specifications
{
    public class CompanyNameMustBeUnique : ISpecification<ICompanyCommand>
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyNameMustBeUnique(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<bool> IsSatisfiedByAsync(ICompanyCommand command)
        {
            var company = await _companyRepository.GetCompany(command.CompanyName);

            var allow = company == null;

            return allow;
        }

        public string ErrorMessage(ICompanyCommand obj)
        {
            return "Company name must be unique";
        }

        public ErrorCode ErrorCode { get; } = ErrorCode.FieldMustUnique;
    }
}
