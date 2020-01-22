using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Request;

namespace SmartELock.Service.Api.Mapper
{
    public interface ICompanyMapper
    {
        CompanyCreateCommand MapToCreateCommand(CompanyPostDto companyPostDto);
    }
}
