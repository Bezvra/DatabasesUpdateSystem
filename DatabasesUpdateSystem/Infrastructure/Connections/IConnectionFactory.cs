using System.Data;

namespace DatabasesUpdateSystem.Infrastructure.Connections
{
    public interface IConnectionFactory
    {
        IDbConnection GetConnection { get; }

        string ConnectionString { get; }
    }
}