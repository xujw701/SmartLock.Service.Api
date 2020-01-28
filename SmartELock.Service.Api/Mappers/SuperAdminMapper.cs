using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public class SuperAdminMapper : ISuperAdminMapper
    {
        public SuperAdminCreateCommand MapToCreateCommand(SuperAdminPostDto superAdminPostDto)
        {
            return new SuperAdminCreateCommand
            {
                Username = superAdminPostDto.Username,
                Password = superAdminPostDto.Password
            };
        }

        public SuperAdminLoginCommand MapToLoginCommand(SuperAdminPostDto superAdminPostDto)
        {
            return new SuperAdminLoginCommand
            {
                Username = superAdminPostDto.Username,
                Password = superAdminPostDto.Password
            };
        }

        public KeyboxAssetCreateCommand MapToKeyboxAssetCreateCommand(KeyboxAssetPostDto keyboxAssetPostDto)
        {
            return new KeyboxAssetCreateCommand
            {
                Uuid = keyboxAssetPostDto.Uuid
            };
        }
    }
}