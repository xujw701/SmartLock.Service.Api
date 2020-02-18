using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Commands;
using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Services
{
    public interface IOtherService
    {
        Task<int> CreateFeedback(FeedbackCreateCommand command);
    }
}
