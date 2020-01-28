using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/branches")]
    public class BranchController : BaseController
    {
        private readonly IBranchService _branchService;

        private readonly IBranchMapper _branchMapper;

        public BranchController(IAuthorizationService authorizationService, IBranchService branchService, IBranchMapper branchMapper) : base(authorizationService)
        {
            _branchService = branchService;

            _branchMapper = branchMapper;
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateBranch(BranchPostDto branchPostDto)
        {
            await ValidateToken(Request.Headers);

            var command = _branchMapper.MapToCreateCommand(branchPostDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var id = await _branchService.CreateBranch(command);

            var response = new DefaultCreatedPostResponseDto
            {
                Id = id
            };

            return Created($"{id}", response);
        }
    }
}