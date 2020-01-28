using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public interface ISuperAdminMapper
    {
        SuperAdminCreateCommand MapToCreateCommand(SuperAdminPostDto superAdminPostDto);
        SuperAdminLoginCommand MapToLoginCommand(SuperAdminPostDto superAdminPostDto);
        KeyboxAssetCreateCommand MapToKeyboxAssetCreateCommand(KeyboxAssetPostDto keyboxAssetPostDto);
    }
}
