using System;
using System.Threading.Tasks;

namespace SmartELock.Core.Repositories.Infrastructure
{
	public interface IDbRetryHandler
	{
		Task<T> QueryAsync<T>(Func<IRetryHandlerConnection, Task<T>> queryFunc);
		Task CommandAsync(Func<IRetryHandlerConnection, Task> command);
		Task TransactionalCommandAsync(Func<IRetryHandlerConnection, Task> command);
	}
}