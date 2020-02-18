using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class PropertyFeedback
    {
        public int PropertyFeedbackId { get; private set; }
        public int PropertyId { get; private set; }
        public int UserId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private PropertyFeedback(PropertyFeedbackCreateCommand command)
        {
            PropertyId = command.PropertyId;
            UserId = command.OperatedBy ?? 0;
            Content = command.Content;
        }

        public static PropertyFeedback CreateFrom(PropertyFeedbackCreateCommand command)
        {
            return new PropertyFeedback(command);
        }
    }
}