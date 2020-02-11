using DatabasesUpdateSystem.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DatabasesUpdateSystem.Infrastructure.Connections
{
    public class MSSQLConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<MSSQLConnectionFactory> _logger;

        private bool disposedValue; // To detect redundant calls

        public Databases DatabaseType { get; private set; }

        public MSSQLConnectionFactory(IConfiguration configuration, ILogger<MSSQLConnectionFactory> logger)
        {
            _configuration = configuration;
            _logger = logger;
            DatabaseType = Databases.MSSQL;
        }

        public IDbConnection GetConnection { get; }

        public string ConnectionString
        {
            get
            {
                var connectionString = _configuration.GetConnectionString("DataBase");
                var builder = new SqlConnectionStringBuilder(connectionString);
                return builder.ConnectionString;
            }
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~ConnectionFactory() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion
    }
}