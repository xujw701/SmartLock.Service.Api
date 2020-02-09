using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/keyboxes")]
    public class KeyboxController : BaseController
    {
        private readonly IKeyboxService _keyboxService;

        private readonly IKeyboxMapper _keyboxMapper;

        public KeyboxController(IAuthorizationService authorizationService, IKeyboxService keyboxService, IKeyboxMapper keyboxMapper) : base(authorizationService)
        {
            _keyboxService = keyboxService;

            _keyboxMapper = keyboxMapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> RegisterKeybox(KeyboxPostDto keyboxPostDto)
        {
            await ValidateToken(Request.Headers, adminOnly: true);

            var command = _keyboxMapper.MapToCreateCommand(keyboxPostDto);

            var id = await _keyboxService.RegisterKeybox(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }
    }
}