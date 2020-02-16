using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Net;
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
        public async Task<IHttpActionResult> CreateKeybox(KeyboxPostDto keyboxPostDto)
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

        [HttpPost]
        [Route("{keyboxId}/assignTo/{userId}")]
        public async Task<IHttpActionResult> AssignTo(int keyboxId, int userId)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToAssignToCommand(keyboxId, userId);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.AssignTo(command);

            if (result)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        [Route("{keyboxId}/property")]
        public async Task<IHttpActionResult> CreateKeyboxProperty(int keyboxId, KeyboxPropertyPostPutDto keyboxPropertyPostPutDto)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxPropertyCreateCommand(keyboxId, keyboxPropertyPostPutDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var id = await _keyboxService.CreateKeyboxProperty(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }

        [HttpPut]
        [Route("{keyboxId}/property/{propertyId}")]
        public async Task<IHttpActionResult> UpdateKeyboxProperty(int keyboxId, int propertyId, KeyboxPropertyPostPutDto keyboxPropertyPostPutDto)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxPropertyUpdateCommand(keyboxId, propertyId, keyboxPropertyPostPutDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.UpdateKeyboxProperty(command);

            if (result)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.InternalServerError);
        }

        [HttpDelete]
        [Route("{keyboxId}/property/{propertyId}")]
        public async Task<IHttpActionResult> EndKeyboxProperty(int keyboxId, int propertyId)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxPropertyDeleteCommand(keyboxId, propertyId);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.EndKeyboxProperty(command);

            if (result)
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            return StatusCode(HttpStatusCode.InternalServerError);
        }
    }
}