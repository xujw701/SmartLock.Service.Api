using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Services;
using SmartELock.Service.Api.Dto.Requests;
using SmartELock.Service.Api.Dto.Responses;
using SmartELock.Service.Api.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartELock.Service.Api.Controllers
{
    [RoutePrefix("api/v1/branches")]
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
        public async Task<IHttpActionResult> CreateBranch(BranchPostPutDto branchPostPutDto)
        {
            await ValidateToken(Request.Headers);

            var command = _branchMapper.MapToCreateCommand(branchPostPutDto);

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

        [HttpPut]
        [Route("{branchId}")]
        public async Task<IHttpActionResult> UpdateBranch(int branchId, BranchPostPutDto branchPostPutDto)
        {
            await ValidateToken(Request.Headers);

            var command = _branchMapper.MapToUpdateCommand(branchId, branchPostPutDto);

            // Inject the operater id
            command.OperatedBy = UserId;
            command.OperatedByAdmin = AdminId;

            var result = await _branchService.UpdateBranch(command);

            if (result)
            {
                return Ok();
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("")]
        public async Task<List<Branch>> GetBranches()
        {
            await ValidateToken(Request.Headers);

            return await _branchService.GetBranches(CurrentUser);
        }
    }
}