using MySqlConnector;
namespace KafeshkaV2.Infrastructure;

public interface IDatabaseConnectionFactory
{
    MySqlConnection CreateConnection();
}