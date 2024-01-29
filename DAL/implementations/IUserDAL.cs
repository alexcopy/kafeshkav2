using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.implementations;

public class IUserDAL : IUser
{
    public IEnumerable<User> FindByEmail(string email)
    {
        using (var connection = DBConnection.CreateConnection())
        {
            return connection.Query<User>("select * from [User] where email = @e", new { e = email });
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