using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;
using MySql.Data.MySqlClient;

namespace KafeshkaV2.DAL.implementations;

public class UserDal : IUserDAL
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
                return connection.Query<User>("SELECT * FROM User WHERE email = @e", new { e = email });
           
        }
    }

    public User FindById(int id)
    {
        using (var connection = DBConnection.CreateConnection())
        {
            return connection.Query<User>("select * from [User] where UserId = @id", new { id = id }).FirstOrDefault();
        }
    }
}