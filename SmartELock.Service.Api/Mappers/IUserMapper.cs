using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public interface IUserMapper
    {
        UserCreateCommand MapToCreateCommand(UserPostDto userPostDto);
        UserLoginCommand MapToLoginCommand(UserTokenPostDto userLoginPostDto);
    }
}
