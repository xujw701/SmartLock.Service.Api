using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Service.Api.Dto.Requests;

namespace SmartELock.Service.Api.Mappers
{
    public interface IOtherMapper
    {
        FeedbackCreateCommand MapToCreateCommand(int userId, FeedbackPostDto feedbackPostDto);
    }
}
