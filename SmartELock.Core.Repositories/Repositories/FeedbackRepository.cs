using SmartELock.Core.Domain.Models;
using SmartELock.Core.Domain.Models.Snapshots;
using SmartELock.Core.Domain.Repositories;
using SmartELock.Core.Repositories.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IDbRetryHandler _dbRetryHandler;

        public FeedbackRepository(IDbRetryHandler dbRetryHandler)
        {
            _dbRetryHandler = dbRetryHandler;
        }

        public async Task<int> CreateFeedback(Feedback feedback)
        {
            var id = await _dbRetryHandler.QueryAsync(async connection =>
            {
                using (var reader = await connection.QueryMultipleAsync("Feedback_Create", new
                {
                    feedback.UserId,
                    feedback.Content
                }))
                {
                    return reader.Read<int>().Single();
                }
            });

            return id;
        }
    }
}
