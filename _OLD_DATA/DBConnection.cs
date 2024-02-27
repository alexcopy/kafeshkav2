using MySql.Data.MySqlClient;

public class DBConnection
{
    public static MySqlConnection CreateConnection()
    {
        string connectionString = "Server=localhost;Port=3386;Database=kafeshkav2;User ID=kafeshka;Password=test123;";

        return new MySqlConnection(connectionString);
    }
}