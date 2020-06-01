using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands.Base;
using SmartELock.Core.Domain.Models.Enums;
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
    [RoutePrefix("api/v1/keyboxes")]
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

        [HttpGet]
        [Route("")]
        public async Task<KeyboxResponseDto> GetKeybox(int? keyboxId = null, string uuid = null)
        {
            await ValidateToken(Request.Headers);

            if (keyboxId.HasValue && !string.IsNullOrEmpty(uuid))
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var command = _keyboxMapper.MapToGetCommand(keyboxId.HasValue ? keyboxId.Value : 0, uuid);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var keybox = await _keyboxService.GetKeybox(command);

            if (keybox == null) return null;

            return new KeyboxResponseDto
            {
                KeyboxId = keybox.KeyboxId,
                CompanyId = keybox.CompanyId,
                BranchId = keybox.BranchId,
                UserId = keybox.UserId,
                Uuid = keybox.Uuid,
                PropertyId = keybox.PropertyId,
                PropertyAddress = keybox.Address,
                KeyboxName = keybox.KeyboxName,
                BatteryLevel = keybox.BatteryLevel,
            };
        }

        [HttpPut]
        [Route("{keyboxId}")]
        public async Task<IHttpActionResult> UpdateKeybox(int keyboxId, KeyboxPutDto keyboxPutDto)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToUpdateCommand(keyboxId, keyboxPutDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.UpdateKeybox(command);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPut]
        [Route("{keyboxId}/pin")]
        public async Task<IHttpActionResult> UpdatePin(int keyboxId, KeyboxPinPutDto keyboxPinPutDto)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToUpdateCommand(keyboxId, keyboxPinPutDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.UpdateKeyboxPin(command);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("mine")]
        public async Task<List<KeyboxResponseDto>> MyKeyboxes()
        {
            await ValidateToken(Request.Headers);

            var keyboxes = await _keyboxService.GetKeyboxes(CurrentUser, UserId);

            return keyboxes.Select(keybox => new KeyboxResponseDto
            {
                KeyboxId = keybox.KeyboxId,
                CompanyId = keybox.CompanyId,
                BranchId = keybox.BranchId,
                UserId = keybox.UserId,
                Uuid = keybox.Uuid,
                PropertyId = keybox.PropertyId,
                PropertyAddress = keybox.Address,
                KeyboxName = keybox.KeyboxName,
                BatteryLevel = keybox.BatteryLevel,
            }).ToList();
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<List<KeyboxResponseDto>> GetKeyboxes(int userId)
        {
            await ValidateToken(Request.Headers);

            var keyboxes = await _keyboxService.GetKeyboxes(CurrentUser, userId);

            return keyboxes.Select(keybox => new KeyboxResponseDto
            {
                KeyboxId = keybox.KeyboxId,
                CompanyId = keybox.CompanyId,
                BranchId = keybox.BranchId,
                UserId = keybox.UserId,
                Uuid = keybox.Uuid,
                PropertyId = keybox.PropertyId,
                PropertyAddress = keybox.Address,
                KeyboxName = keybox.KeyboxName,
                BatteryLevel = keybox.BatteryLevel,
            }).ToList();
        }

        [HttpGet]
        [Route("iunlocked")]
        public async Task<List<KeyboxResponseDto>> KeyboxesIUnlocked()
        {
            await ValidateToken(Request.Headers);

            var keyboxes = await _keyboxService.GetKeyboxesIUnlocked(UserId);

            return keyboxes.Select(keybox => new KeyboxResponseDto
            {
                KeyboxId = keybox.KeyboxId,
                CompanyId = keybox.CompanyId,
                BranchId = keybox.BranchId,
                UserId = keybox.UserId,
                Uuid = keybox.Uuid,
                PropertyId = keybox.PropertyId,
                PropertyAddress = keybox.Address,
                KeyboxName = keybox.KeyboxName,
                BatteryLevel = keybox.BatteryLevel,
                AcessUserId = keybox.AcessUserId,
                InOn = keybox.InOn
            }).ToList();
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
                return Ok();
            }
            return InternalServerError();
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

        [HttpGet]
        [Route("{keyboxId}/property/{propertyId}")]
        public async Task<PropertyResponseDto> GetKeyboxProperty(int keyboxId, int propertyId)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxPropertyCommand(keyboxId, propertyId);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var property = await _keyboxService.GetKeyboxProperty(command);

            if (property == null) return null;

            return new PropertyResponseDto
            {
                PropertyId = property.PropertyId,
                PropertyName = property.PropertyName,
                Address = property.Address,
                Notes = property.Notes,
                Price = property.Price,
                Bedrooms = property.Bedrooms,
                Bathrooms = property.Bathrooms,
                FloorArea = property.FloorArea,
                LandArea = property.LandArea,
            };
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
                return Ok();
            }
            return InternalServerError();
        }

        [HttpDelete]
        [Route("{keyboxId}/property/{propertyId}")]
        public async Task<IHttpActionResult> EndKeyboxProperty(int keyboxId, int propertyId)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxPropertyCommand(keyboxId, propertyId);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.EndKeyboxProperty(command);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("{keyboxId}/property/{propertyId}/resources")]
        public async Task<List<ResProperty>> GetPropertyResource(int keyboxId, int propertyId)
        {
            // Validate token
            await ValidateToken(Request.Headers);

            return await _keyboxService.GetPropertyResource(propertyId);
        }

        [HttpPost]
        [Route("{keyboxId}/property/{propertyId}/resources")]
        public async Task<IHttpActionResult> AddPropertyResource(int keyboxId, int propertyId, FileType fileType = FileType.Jpeg)
        {
            // Validate token
            await ValidateToken(Request.Headers);

            var bytes = await GetBodyBytes();

            var result = await _keyboxService.AddPropertyResource(propertyId, bytes, fileType);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpPut]
        [Route("{keyboxId}/property/{propertyId}/resources/{resPropertyId}")]
        public async Task<IHttpActionResult> EditPropertyResource(int keyboxId, int propertyId, int resPropertyId, FileType fileType = FileType.Jpeg)
        {
            // Validate token
            await ValidateToken(Request.Headers);

            var bytes = await GetBodyBytes();

            var result = await _keyboxService.UpdatePropertyResource(propertyId, resPropertyId, bytes, fileType);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpDelete]
        [Route("{keyboxId}/property/{propertyId}/resources/{resPropertyId}")]
        public async Task<IHttpActionResult> DeletePropertyResource(int keyboxId, int propertyId, int resPropertyId)
        {
            // Validate token
            await ValidateToken(Request.Headers);

            var result = await _keyboxService.DeletePropertyResource(propertyId, resPropertyId);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("property/resources/{resPropertyId}")]
        public async Task<byte[]> GetPropertyResource(int resPropertyId)
        {
            // Validate token
            await ValidateToken(Request.Headers);

            return await _keyboxService.GetPropertyResourceData(resPropertyId);
        }

        [HttpPost]
        [Route("{keyboxId}/unlock/permission")]
        public async Task<LockUnlockResponseDto> UnlockPermission(int keyboxId)
        {
            await ValidateToken(Request.Headers);

            var command = new KeyboxCommand();

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.UnlockPermission(CurrentUser, command);

            return new LockUnlockResponseDto
            {
                Success = result
            };
        }

        [HttpPost]
        [Route("{keyboxId}/unlock")]
        public async Task<LockUnlockResponseDto> Unlock(int keyboxId, KeyboxHistoryPostDto keyboxHistoryPostDto)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxHistoryCommand(keyboxId, keyboxHistoryPostDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.Unlock(CurrentUser, command);

            return new LockUnlockResponseDto
            {
                Success = result
            };
        }

        [HttpPost]
        [Route("{keyboxId}/lock")]
        public async Task<LockUnlockResponseDto> Lock(int keyboxId, KeyboxHistoryPostDto keyboxHistoryPostDto)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxHistoryCommand(keyboxId, keyboxHistoryPostDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _keyboxService.Lock(command);

            return new LockUnlockResponseDto
            {
                Success = result
            };
        }

        [HttpGet]
        [Route("{keyboxId}/property/{propertyId}/histories")]
        public async Task<List<KeyboxHistoryResponseDto>> GetHistories(int keyboxId, int propertyId)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxPropertyCommand(keyboxId, propertyId);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var histories = await _keyboxService.GetKeyboxHistories(command);

            if (histories == null) return null;

            return histories.Select(history => new KeyboxHistoryResponseDto
            {
                KeyboxHistoryId = history.KeyboxHistoryId,
                KeyboxId = history.KeyboxId,
                UserId = history.UserId,
                TmpUserId = history.TmpUserId,
                PropertyId = history.PropertyId,
                FirstName = history.FirstName,
                LastName = history.LastName,
                ResPortraitId = history.ResPortraitId,
                InOn = history.InOn,
                OutOn = history.OutOn
            }).ToList();
        }

        [HttpPost]
        [Route("{keyboxId}/property/{propertyId}/feedback")]
        public async Task<IHttpActionResult> CreateFeedback(int keyboxId, int propertyId, FeedbackPostDto feedbackPostDto)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToPropertyFeedbackCreateCommand(keyboxId, propertyId, feedbackPostDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var id = await _keyboxService.CreatePropertyFeedback(CurrentUser, command);

            if (id > 0)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("{keyboxId}/property/{propertyId}/feedback")]
        public async Task<List<PropertyFeedbackResponseDto>> GetFeedback(int keyboxId, int propertyId)
        {
            await ValidateToken(Request.Headers);

            var command = _keyboxMapper.MapToKeyboxPropertyCommand(keyboxId, propertyId);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var feedbacks = await _keyboxService.GetKeyboxPropertyFeedback(command);

            if (feedbacks == null) return null;

            return feedbacks.Select(feedback => new PropertyFeedbackResponseDto
            {
                PropertyFeedbackId = feedback.PropertyFeedbackId,
                PropertyId = feedback.PropertyId,
                UserId = feedback.UserId,
                FirstName = feedback.FirstName,
                LastName = feedback.LastName,
                Phone = feedback.Phone,
                ResPortraitId = feedback.ResPortraitId,
                Content = feedback.Content,
                CreatedOn = feedback.CreatedOn
            }).ToList();
        }
    }
}