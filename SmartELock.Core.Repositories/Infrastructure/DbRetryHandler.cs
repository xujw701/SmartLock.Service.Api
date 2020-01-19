using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SmartELock.Core.Domain.Models.Exceptions;
using Polly;

namespace SmartELock.Core.Repositories.Infrastructure
{
	public class DbRetryHandler : IDbRetryHandler
	{
		private readonly Policy _asyncPolicy;
		private readonly Random _random;
		private readonly TimeSpan _baseTime;
		private readonly IConnectionFactory _connectionFactory;

		public DbRetryHandler(IConnectionFactory connectionFactory) : this(TimeSpan.FromSeconds(2), connectionFactory) { }

		public DbRetryHandler(TimeSpan baseWaitTime, IConnectionFactory connectionFactory)
		{
			_baseTime = baseWaitTime;
			_connectionFactory = connectionFactory;

			_asyncPolicy = Policy
				.Handle<SqlException>(CanRetry)
				.WaitAndRetryAsync(5, RetryWaitTime, (exception, timeSpan) =>
				{
					// Logger.Warn(exception, "Database error, retrying in {0:F2} seconds", timeSpan.TotalSeconds);
				});

			_random = new Random();
		}

		public async Task<T> QueryAsync<T>(Func<IRetryHandlerConnection, Task<T>> queryFunc)
		{
			try
			{
				using (var connection = _connectionFactory.GetConnection())
				{
					await connection.OpenAsync();
					var retryConnectionHandler = new RetryHandlerConnection(connection);
					return await _asyncPolicy.ExecuteAsync(async () => await queryFunc(retryConnectionHandler));
				}
			}
			catch (SqlException e)
			{
				ProcessException(e);
				throw;
			}
		}

		public async Task CommandAsync(Func<IRetryHandlerConnection, Task> command)
		{
			try
			{
				using (DbConnection connection = _connectionFactory.GetConnection())
				{
					await connection.OpenAsync();
					var retryConnectionHandler = new RetryHandlerConnection(connection);
					await _asyncPolicy.ExecuteAsync(() => command(retryConnectionHandler));
				}
			}
			catch (SqlException e)
			{
				ProcessException(e);
				throw;
			}
		}

		public async Task TransactionalCommandAsync(Func<IRetryHandlerConnection, Task> command)
		{
			try
			{
				using (DbConnection connection = _connectionFactory.GetConnection())
				{
					await connection.OpenAsync();
					var transaction = connection.BeginTransaction();
					try
					{
						var retryConnectionHandler = new RetryHandlerConnection(connection, transaction);
						await _asyncPolicy.ExecuteAsync(() => command(retryConnectionHandler));
						transaction.Commit();
					}
					catch (Exception)
					{
						if (transaction.Connection != null)
						{
							transaction.Rollback();
						}

						throw;
					}
				}
			}
			catch (SqlException e)
			{
				ProcessException(e);
				throw;
			}
		}

		private void ProcessException(SqlException e)
		{
			if (e.State == (int)SqlExceptionState.EntityConflict)
			{
				throw new ConflictEntityException(e.Message);
			}

			if (e.State == (int)SqlExceptionState.EntityNotFound)
			{
				throw new EntityNotFoundException(e.Message);
			}

			if (e.State == (int)SqlExceptionState.DataValidation)
			{
				throw new DataValidationException(e.Message);
			}
		}

		private TimeSpan RetryWaitTime(int retryAttempt)
		{
			var randomFactor = 0.5 + _random.NextDouble();
			var timeMultiplier = Math.Pow(2, retryAttempt - 1) * randomFactor;
			return TimeSpan.FromTicks((long)(_baseTime.Ticks * timeMultiplier));
		}

		private static bool CanRetry(SqlException exception)
		{
			// Determine whether or not we should retry after a particular exception
			// To see all possible errors, run:
			//     SELECT * FROM SYSMESSAGES
			//     WHERE msglangid = 1033
			//     ORDER BY severity, error
			var transientErrors = new int[]
			{
				-2,     // Timeout completing request
				1205,   // Transaction was deadlocked
				4060,   // Cannot open database
				40197,  // Error processing request, could be due to upgrade or failover running
				40501,  // Service is busy
				40613,  // Database unavailable
			};
			return exception.Errors.Cast<SqlError>().All(
				error => transientErrors.Contains(error.Number));
		}
	}
}
