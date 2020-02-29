using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class ResProperty
    {
        public int ResPropertyId { get; private set; }
        public int PropertyId { get; private set; }
        public string Url { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private ResProperty(ResPropertySnapshot snapshot)
        {
            ResPropertyId = snapshot.ResPropertyId;
            PropertyId = snapshot.PropertyId;
            Url = snapshot.Url;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static ResProperty CreateFrom(ResPropertySnapshot snapshot)
        {
            return new ResProperty(snapshot);
        }
    }
}
