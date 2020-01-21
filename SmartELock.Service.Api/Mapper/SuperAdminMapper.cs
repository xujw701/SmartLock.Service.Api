using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Request;

namespace SmartELock.Service.Api.Mapper
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
    }
}