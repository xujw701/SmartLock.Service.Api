using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Request;

namespace SmartELock.Service.Api.Mapper
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