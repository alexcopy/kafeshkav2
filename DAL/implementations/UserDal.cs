using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;
using MySql.Data.MySqlClient;

namespace KafeshkaV2.DAL.implementations;
public class UserDal : IUserDal
{
    private readonly string _connectionString;

    public UserDal(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<User> FindByEmail(string email)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            return connection.Query<User>("SELECT * FROM `User` WHERE email = @Email", new { Email = email });
        }
    }

    public User FindById(int id)
    {
        using (var connection = new MySqlConnection(_connectionString))
        {
            return connection.Query<User>("SELECT * FROM `User` WHERE UserId = @Id", new { Id = id }).FirstOrDefault();
        }
    }
}