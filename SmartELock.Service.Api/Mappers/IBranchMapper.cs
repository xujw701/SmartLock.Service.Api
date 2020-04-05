using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public interface IBranchMapper
    {
        BranchCreateCommand MapToCreateCommand(BranchPostPutDto branchPostPutDto);
        BranchUpdateCommand MapToUpdateCommand(int branchId, BranchPostPutDto branchPostPutDto);
    }
}
