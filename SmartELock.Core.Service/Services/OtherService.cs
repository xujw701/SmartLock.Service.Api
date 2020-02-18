using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Domain.Services;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Services.Services
{
    public class OtherService : IOtherService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public OtherService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        public async Task<int> CreateFeedback(FeedbackCreateCommand command)
        {
            var feedback = Feedback.CreateFrom(command);

            return await _feedbackRepository.CreateFeedback(feedback);
        }
    }
}
