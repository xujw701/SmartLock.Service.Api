using SmartELock.Core.Domain.Models.Commands.Base;

namespace SmartELock.Core.Domain.Models.Commands
{
    public class KeyboxUpdateCommand : KeyboxCommand, IKeyboxUpdateCommand, IKeyboxAssetCommand
    {
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int UserId { get; set; }
        public string KeyboxName { get; set; }
        public string Pin { get; set; }
    }
}