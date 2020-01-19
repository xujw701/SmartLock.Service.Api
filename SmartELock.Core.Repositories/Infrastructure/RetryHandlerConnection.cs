using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;

namespace SmartELock.Core.Repositories.Infrastructure
{
	public class RetryHandlerConnection : IRetryHandlerConnection
	{
		private readonly DbConnection _connection;
		private readonly IDbTransaction _transaction;

		public RetryHandlerConnection(DbConnection connection, IDbTransaction transaction = null)
		{
			_connection = connection;
			_transaction = transaction;
		}

		public Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure)
		{
			return _connection.QueryMultipleAsync(sql, param, commandType: commandType);
		}

		public Task<int> ExecuteAsync(string sql, object param, CommandType commandType = CommandType.StoredProcedure)
		{
			return _connection.ExecuteAsync(sql, param, commandType: commandType, transaction: _transaction);
		}
	}
}