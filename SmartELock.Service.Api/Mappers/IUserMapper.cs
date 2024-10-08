﻿using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public interface IUserMapper
    {
        UserCreateCommand MapToCreateCommand(UserPostDto userPostDto);
        UserMeUpdateCommand MapToMeUpdateCommand(int userId, UserMePutDto userMePutDto);
        UserUpdateCommand MapToUpdateCommand(int userId, UserPutDto userPutDto);
        UserLoginCommand MapToLoginCommand(UserTokenPostDto userLoginPostDto);
    }
}
