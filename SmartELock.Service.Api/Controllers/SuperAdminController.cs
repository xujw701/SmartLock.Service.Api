using SmartELock.Core.Domain.Service;
using SmartELock.Service.Api.Dto.Request;
using SmartELock.Service.Api.Dto.Response;
using SmartELock.Service.Api.Mapper;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/superadmins")]
    public class SuperAdminController : ApiController
    {
        private readonly ISuperAdminService _superAdminService;

        private readonly ISuperAdminMapper _superAdminMapper;

        public SuperAdminController(ISuperAdminService superAdminService, ISuperAdminMapper superAdminMapper)
        {
            _superAdminService = superAdminService;

            _superAdminMapper = superAdminMapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateSuperAdmin(SuperAdminPostDto superAdminPostDto)
        {
            var command = _superAdminMapper.MapToCreateCommand(superAdminPostDto);

            var id = await _superAdminService.CreateSuperAdmin(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(SuperAdminPostDto superAdminPostDto)
        {
            var loginCommand = _superAdminMapper.MapToLoginCommand(superAdminPostDto);

            return StatusCode(HttpStatusCode.Accepted);
        }
    }
}