using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class Branch
    {
        public int BranchId { get; private set; }
        public int CompanyId { get; private set; }
        public string BranchName { get; private set; }
        public string Address { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private Branch(BranchCreateCommand command)
        {
            CompanyId = command.CompanyId;
            BranchName = command.BranchName;
            Address = command.Address;
        }

        private Branch(BranchUpdateCommand command)
        {
            BranchId = command.BranchId;
            CompanyId = command.CompanyId;
            BranchName = command.BranchName;
            Address = command.Address;
        }

        private Branch(BranchSnapshot snapshot)
        {
            BranchId = snapshot.BranchId;
            CompanyId = snapshot.CompanyId;
            BranchName = snapshot.BranchName;
            Address = snapshot.Address;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static Branch CreateFrom(BranchCreateCommand command)
        {
            return new Branch(command);
        }

        public static Branch CreateFrom(BranchUpdateCommand command)
        {
            return new Branch(command);
        }

        public static Branch CreateFrom(BranchSnapshot snapshot)
        {
            return new Branch(snapshot);
        }
    }
}
