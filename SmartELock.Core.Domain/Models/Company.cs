using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class Company
    {
        public int CompanyId { get; private set; }
        public string CompanyName { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private Company(CompanyCreateCommand command)
        {
            CompanyName = command.CompanyName;
        }

        private Company(CompanySnapshot snapshot)
        {
            CompanyId = snapshot.CompanyId;
            CompanyName = snapshot.CompanyName;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static Company CreateFrom(CompanyCreateCommand command)
        {
            return new Company(command);
        }

        public static Company CreateFrom(CompanySnapshot snapshot)
        {
            return new Company(snapshot);
        }
    }
}
