using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Models.Snapshots;
using System;

namespace SmartELock.Core.Domain.Models
{
    public class Feedback
    {
        public int FeedbackId { get; private set; }
        public int UserId { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        private Feedback(FeedbackCreateCommand command)
        {
            UserId = command.UserId;
            Content = command.Content;
        }

        public static Feedback CreateFrom(FeedbackCreateCommand command)
        {
            return new Feedback(command);
        }
    }
}
