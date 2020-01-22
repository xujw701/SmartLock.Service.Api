using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
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
        public async Task<SuperAdminLoginResponseDto> Login(SuperAdminPostDto superAdminPostDto)
        {
            var command = _superAdminMapper.MapToLoginCommand(superAdminPostDto);

            var superAdmin = await _superAdminService.Login(command);

            if (superAdmin == null)
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            return new SuperAdminLoginResponseDto
            {
                SuperAdminId = superAdmin.SuperAdminId,
                Token = superAdmin.Token ?? string.Empty
            };
        }
    }
}