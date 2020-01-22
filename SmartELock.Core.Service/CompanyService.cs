using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Exceptions;
using SmartELock.Core.Domain.Repository;
using SmartELock.Core.Domain.Service;
using SmartELock.Core.Services.Validators;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly ICommandValidator<CompanyCreateCommand> _companyCreateValidator;

        public CompanyService(ICompanyRepository companyRepository, ICommandValidator<CompanyCreateCommand> companyCreateValidator)
        {
            _companyRepository = companyRepository;

            _companyCreateValidator = companyCreateValidator;
        }

        public async Task<int> CreateCompany(CompanyCreateCommand command)
        {
            var validationResult = await _companyCreateValidator.Validate(command);

            if (!validationResult.IsValid)
            {
                throw new DomainValidationException(validationResult.ErrorMessage, validationResult.ErrorCode);
            }

            var company = Company.CreateFrom(command);

            return await _companyRepository.CreateCompany(company);
        }
    }
}
