using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace SmartELock.Core.Repositories.Infrastructure
{
	public interface IRetryHandlerConnection
	{
		Task<SqlMapper.GridReader> QueryMultipleAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure);

		Task<int> ExecuteAsync(string sql, object param = null, CommandType commandType = CommandType.StoredProcedure);
	}
}