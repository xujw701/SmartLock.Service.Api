using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/users")]
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
    }
}