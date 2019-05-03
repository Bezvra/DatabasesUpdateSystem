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
        private readonly IDbConnection externalConnection;
        protected readonly ILogger<Repository> _logger;

        public Repository(ILogger<Repository> logger)
        {
            _logger = logger;
            QueryLimit = 300; // Queries taking longer than 300ms are logged
            QueryLimitIncludeStackTrace = false; // include stack trace when logging expensive queries
        }

        protected string DBConnectionString { get; set; }
        protected string StorageConnectionString { get; set; }

        /// <summary>
        ///     Set to non-zero to log queries using more time than the value specified (milliseconds)
        /// </summary>
        public int QueryLimit { get; set; }

        /// <summary>
        ///     When an expensive query is detected, include complete call stack.
        /// </summary>
        public bool QueryLimitIncludeStackTrace { get; set; }

        public IDbConnection OpenDBConnection()
        {
            // CAUTION:
            //
            // It's tempting to reuse OpenConnection(connectionString), but if you do the logged
            // stacktrace will be offset by 1.

            var conn = externalConnection ?? new SqlConnection(DBConnectionString);

            // Only dispose the connection if we own it
            var disposeConnection = externalConnection == null;
            if (externalConnection == null || externalConnection.State != ConnectionState.Open)
            {
                conn.Open();
            }

            return conn;
        }

        public DateTime GetDateFromTimeStamp(string s)
        {
            var days = int.Parse(s.Substring(0, 5));
            return new DateTime(1980, 1, 1).AddDays(days);
        }

        public string GetTimeStamp()
        {
            var dateDiff = DateTime.UtcNow - new DateTime(1980, 1, 1);
            return dateDiff.Days.ToString("00000") + DateTime.UtcNow.ToString("hhmmss");
        }

        public string GetTimeFromTimeStamp(string s)
        {
            //if length < 11 chars then it is not possible get time
            if (string.IsNullOrEmpty(s) || s.Length < 11)
            {
                return string.Empty;
            }

            var time = s.Substring(5, 2) + ":" + s.Substring(7, 2) + ":" + s.Substring(9, 2);
            return time;
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
