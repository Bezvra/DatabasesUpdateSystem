using DatabasesUpdateSystem.Domain.Enums;
using DatabasesUpdateSystem.Infrastructure.Connections;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Infrastructure.Repositories
{
    public class Repository
    {
        protected readonly ILogger<Repository> _logger;

        public Repository(ILogger<Repository> logger)
        {
            _logger = logger;
        }

        protected IConnectionFactory DBContext { get; set; }
        protected IConnectionFactory StorageContext { get; set; }

        public IDbConnection OpenConnection()
        {
            IDbConnection connection = null;
            switch (DBContext.DatabaseType)
            {
                case Databases.MSSQL:
                    connection = new SqlConnection(DBContext.ConnectionString);
                    break;
                default:
                    break;
            }

            if (connection != null || connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            return connection;
        }

        public void InTransaction(Action<IDbConnection, IDbTransaction> executor)
        {
            using (var connection = OpenConnection())
            {
                connection.Open();
                var transaction = connection.BeginTransaction();
                try
                {
                    executor(connection, transaction);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}