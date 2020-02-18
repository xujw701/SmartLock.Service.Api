using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/v1/feedback")]
    public class FeedbackController : BaseController
    {
        private readonly IOtherService _otherService;

        private readonly IOtherMapper _otherMapper;

        public FeedbackController(IAuthorizationService authorizationService, IOtherService otherService, IOtherMapper otherMapper) : base(authorizationService)
        {
            _otherService = otherService;

            _otherMapper = otherMapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateFeedback(FeedbackPostDto feedbackPostDto)
        {
            await ValidateToken(Request.Headers);

            var command = _otherMapper.MapToCreateCommand(CurrentUser.UserId, feedbackPostDto);

            var id = await _otherService.CreateFeedback(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }
    }
}