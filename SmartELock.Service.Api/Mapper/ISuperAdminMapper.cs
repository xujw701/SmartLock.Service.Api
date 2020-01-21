using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Request;

namespace SmartELock.Service.Api.Mapper
{
    public interface ISuperAdminMapper
    {
        SuperAdminCreateCommand MapToCreateCommand(SuperAdminPostDto superAdminPostDto);
        SuperAdminLoginCommand MapToLoginCommand(SuperAdminPostDto superAdminPostDto);
    }
}
