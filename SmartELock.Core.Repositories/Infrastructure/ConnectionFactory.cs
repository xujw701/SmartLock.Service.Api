using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace SmartELock.Core.Repositories.Infrastructure
{
	public class ConnectionFactory : IConnectionFactory
	{
		private readonly string _connectionString;

		public ConnectionFactory() : this(ConfigurationManager.ConnectionStrings["SmartELockServiceDb"].ConnectionString)
		{ }

		public ConnectionFactory(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbConnection GetConnection()
		{
			return new SqlConnection(_connectionString);
		}
	}
}
