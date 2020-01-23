using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public class BranchMapper : IBranchMapper
    {
        public BranchCreateCommand MapToCreateCommand(BranchPostDto branchPostDto)
        {
            return new BranchCreateCommand
            {
                CompanyId = branchPostDto.CompanyId,
                BranchName = branchPostDto.BranchName,
                Address = branchPostDto.Address
            };
        }
    }
}