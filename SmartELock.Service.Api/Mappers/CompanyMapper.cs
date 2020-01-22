using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public class CompanyMapper : ICompanyMapper
    {
        public CompanyCreateCommand MapToCreateCommand(CompanyPostDto companyPostDto)
        {
            return new CompanyCreateCommand
            {
                CompanyName = companyPostDto.CompanyName,
            };
        }
    }
}