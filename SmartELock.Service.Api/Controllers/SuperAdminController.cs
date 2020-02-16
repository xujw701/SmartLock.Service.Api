using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/v1/superadmins")]
    public class SuperAdminController : BaseController
    {
        private readonly ISuperAdminService _superAdminService;

        private readonly ISuperAdminMapper _superAdminMapper;

        public SuperAdminController(IAuthorizationService authorizationService, ISuperAdminService superAdminService, ISuperAdminMapper superAdminMapper) : base (authorizationService)
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
        [Route("token")]
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

        [HttpPost]
        [Route("keyboxassets")]
        public async Task<IHttpActionResult> CreateKeyboxAsset(KeyboxAssetPostDto keyboxAssetPostDto)
        {
            var command = _superAdminMapper.MapToKeyboxAssetCreateCommand(keyboxAssetPostDto);

            var id = await _superAdminService.CreateKeyboxAsset(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }
    }
}