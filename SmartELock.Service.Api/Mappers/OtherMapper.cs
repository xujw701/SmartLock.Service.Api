using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public class OtherMapper : IOtherMapper
    {
        public FeedbackCreateCommand MapToCreateCommand(int userId, FeedbackPostDto feedbackPostDto)
        {
            return new FeedbackCreateCommand
            {
                UserId = userId,
                Content = feedbackPostDto.Content
            };
        }
    }
}