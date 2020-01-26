using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : BaseAdminController
    {
        private readonly IUserService _userService;

        private readonly IUserMapper _userMapper;

        public UserController(ISuperAdminService superAdminService, IUserService userService, IUserMapper userMapper) : base(superAdminService)
        {
            _userService = userService;

            _userMapper = userMapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateUser(UserPostDto userPostDto)
        {
            var command = _userMapper.MapToCreateCommand(userPostDto);

            var id = await _userService.CreateUser(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }
    }
}