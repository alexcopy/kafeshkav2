using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;
using KafeshkaV2.Infrastructure;
using MySql.Data.MySqlClient;

namespace KafeshkaV2.DAL.implementations;
public class UserDal : IUserDal
{
    private readonly IDatabaseConnectionFactory _databaseConnectionFactory;

    public UserDal(IDatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    public IEnumerable<User> FindByEmail(string email)
    {
        using (var connection = _databaseConnectionFactory.CreateConnection())
        {
            return connection.Query<User>("SELECT * FROM `User` WHERE email = @Email", new { Email = email });
        }
    }

    public User FindById(int id)
    {
        using (var connection = _databaseConnectionFactory.CreateConnection())
        {
            return connection.Query<User>("SELECT * FROM `User` WHERE UserId = @Id", new { Id = id }).FirstOrDefault();
        }
    }
}