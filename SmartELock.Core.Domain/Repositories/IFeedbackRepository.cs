using SmartELock.Core.Domain.Models;
using System.Threading.Tasks;

namespace SmartELock.Core.Domain.Repositories
{
    public interface IFeedbackRepository
    {
        Task<int> CreateFeedback(Feedback feedback);
    }
}
