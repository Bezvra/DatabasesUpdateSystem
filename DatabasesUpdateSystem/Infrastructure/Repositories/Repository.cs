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

        protected string DBConnectionString { get; set; }

        public IDbConnection OpenConnection()
        {
            var connection = new SqlConnection(DBConnectionString);
            connection.Open();

            return connection;
        }

        public void InTransaction(Action<IDbConnection, IDbTransaction> executor)
        {
            using (var connection = new SqlConnection(DBConnectionString))
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