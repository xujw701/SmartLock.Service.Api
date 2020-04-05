using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public class BranchMapper : IBranchMapper
    {
        public BranchCreateCommand MapToCreateCommand(BranchPostPutDto branchPostPutDto)
        {
            return new BranchCreateCommand
            {
                CompanyId = branchPostPutDto.CompanyId,
                BranchName = branchPostPutDto.BranchName,
                Address = branchPostPutDto.Address
            };
        }

        public BranchUpdateCommand MapToUpdateCommand(int branchId, BranchPostPutDto branchPostPutDto)
        {
            return new BranchUpdateCommand
            {
                CompanyId = branchPostPutDto.CompanyId,
                BranchId = branchId,
                BranchName = branchPostPutDto.BranchName,
                Address = branchPostPutDto.Address
            };
        }
    }
}