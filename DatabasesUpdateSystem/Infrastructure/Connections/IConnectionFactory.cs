using DatabasesUpdateSystem.Domain.Enums;

namespace DatabasesUpdateSystem.Infrastructure.Connections
{
    public interface IConnectionFactory
    {
        Databases DatabaseType { get; }

        string ConnectionString { get; }
    }
}