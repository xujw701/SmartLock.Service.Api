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
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public int? ResPortraitId { get; private set; }
        public string Content { get; private set; }
        public bool IsRead { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private PropertyFeedback(PropertyFeedbackCreateCommand command)
        {
            PropertyId = command.PropertyId;
            UserId = command.OperatedBy ?? 0;
            Content = command.Content;
            IsRead = false;
        }

        private PropertyFeedback(PropertyFeedbackSnapshot snapshot)
        {
            PropertyFeedbackId = snapshot.PropertyFeedbackId;
            PropertyId = snapshot.PropertyId;
            UserId = snapshot.UserId;
            FirstName = snapshot.FirstName;
            LastName = snapshot.LastName;
            Phone = snapshot.Phone;
            ResPortraitId = snapshot.ResPortraitId;
            Content = snapshot.Content;
            CreatedOn = snapshot.CreatedOn;
            UpdatedOn = snapshot.UpdatedOn;
        }

        public static PropertyFeedback CreateFrom(PropertyFeedbackCreateCommand command)
        {
            return new PropertyFeedback(command);
        }

        public static PropertyFeedback CreateFrom(PropertyFeedbackSnapshot snapshot)
        {
            return new PropertyFeedback(snapshot);
        }
    }
}