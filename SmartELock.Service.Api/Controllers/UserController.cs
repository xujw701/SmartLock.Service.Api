﻿using SmartELock.Core.Domain.Models.Enums;
using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/v1/users")]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        private readonly IUserMapper _userMapper;

        public UserController(IAuthorizationService authorizationService, IUserService userService, IUserMapper userMapper) : base(authorizationService)
        {
            _userService = userService;

            _userMapper = userMapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateUser(UserPostDto userPostDto)
        {
            await ValidateToken(Request.Headers);

            var command = _userMapper.MapToCreateCommand(userPostDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var id = await _userService.CreateUser(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }

        [HttpPost]
        [Route("token")]
        public async Task<UserTokenResponseDto> Login(UserTokenPostDto userTokenPostDto)
        {
            var command = _userMapper.MapToLoginCommand(userTokenPostDto);

            var user = await _userService.Login(command);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return new UserTokenResponseDto
            {
                UserId = user.UserId,
                Token = user.Token ?? string.Empty
            };
        }

        [HttpPost]
        [Route("auth")]
        public async Task<IHttpActionResult> Auth(UserTokenPostDto userTokenPostDto)
        {
            await ValidateToken(Request.Headers);

            var command = _userMapper.MapToLoginCommand(userTokenPostDto);

            var id = await _userService.Auth(command);

            if (UserId > 0 && id == UserId)
            {
                var response = new DefaultCreatedPostResponseDto
                {
                    Id = id
                };

                return Created($"{id}", response);
            }

            return Unauthorized();
        }

        [HttpGet]
        [Route("me")]
        public async Task<UserResponseDto> GetMe()
        {
            await ValidateToken(Request.Headers);

            return new UserResponseDto
            {
                UserId = CurrentUser.UserId,
                CompanyId = CurrentUser.CompanyId,
                BranchId = CurrentUser.BranchId,
                UserName = CurrentUser.Username,
                FirstName = CurrentUser.FirstName,
                LastName = CurrentUser.LastName,
                Email = CurrentUser.Email,
                Phone = CurrentUser.Phone,
                Individual = CurrentUser.Individual,
                UserRoleId = CurrentUser.UserRoleId,
                ResPortraitId = CurrentUser.ResPortraitId
            };
        }

        [HttpPut]
        [Route("me")]
        public async Task<IHttpActionResult> UpdateMe(UserMePutDto userMePutDto)
        {
            await ValidateToken(Request.Headers);

            var command = _userMapper.MapToMeUpdateCommand(UserId, userMePutDto);

            var result = await _userService.UpdateMe(command);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPost]
        [Route("portrait")]
        public async Task<IHttpActionResult> ChangePortrait(FileType fileType = FileType.Png)
        {
            // Validate token
            await ValidateToken(Request.Headers);

            var bytes = await GetBodyBytes();

            var id = await _userService.UpdatePortrait(UserId, bytes, fileType);

            if (id > 0)
            {
                var response = new DefaultCreatedPostResponseDto
                {
                    Id = id
                };

                return Created($"{id}", response);
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("portrait/{portraitId}")]
        public async Task<byte[]> GetPortrait(int portraitId)
        {
            // Validate token
            await ValidateToken(Request.Headers);

            return await _userService.GetPortrait(portraitId);
        }

        [HttpGet]
        [Route("")]
        public async Task<List<UserResponseDto>> GetUsers(int? branchId = null)
        {
            await ValidateToken(Request.Headers);

            if (!branchId.HasValue) BadRequest();

            var result = await _userService.GetUsers(CurrentUser, branchId.Value);

            return result.Select(user => new UserResponseDto()
            {
                UserId = user.UserId,
                CompanyId = user.CompanyId,
                BranchId = user.BranchId,
                UserName = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Individual = user.Individual,
                UserRoleId = user.UserRoleId,
                ResPortraitId = user.ResPortraitId
            }).ToList();
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task<IHttpActionResult> UpdateUser(int userId, UserPutDto userPutDto)
        {
            await ValidateToken(Request.Headers);

            var command = _userMapper.MapToUpdateCommand(userId, userPutDto);

            var result = await _userService.UpdateUser(CurrentUser, command);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}