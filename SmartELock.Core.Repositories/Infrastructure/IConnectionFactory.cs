using System.Data.Common;

namespace SmartELock.Core.Repositories.Infrastructure
{
	public interface IConnectionFactory
	{
		DbConnection GetConnection();
	}
}