using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public class UserMapper : IUserMapper
    {
        public UserCreateCommand MapToCreateCommand(UserPostDto userPostDto)
        {
            return new UserCreateCommand
            {
                CompanyId = userPostDto.CompanyId,
                BranchId = userPostDto.BranchId,
                FirstName = userPostDto.FirstName,
                LastName = userPostDto.LastName,
                Email = userPostDto.Email,
                Phone = userPostDto.Phone,
                Username = userPostDto.Username,
                Password = userPostDto.Password,
                Individual = userPostDto.Individual,
                UserRoleId = userPostDto.UserRoleId
            };
        }

        public UserMeUpdateCommand MapToMeUpdateCommand(int userId, UserMePutDto userMePutDto)
        {
            return new UserMeUpdateCommand
            {
                UserId = userId,
                FirstName = userMePutDto.FirstName,
                LastName = userMePutDto.LastName,
                Email = userMePutDto.Email,
                Phone = userMePutDto.Phone,
                Password = userMePutDto.Password
            };
        }

        public UserLoginCommand MapToLoginCommand(UserTokenPostDto userLoginPostDto)
        {
            return new UserLoginCommand
            {
                Username = userLoginPostDto.Username,
                Password = userLoginPostDto.Password
            };
        }
    }
}