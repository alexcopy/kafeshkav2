using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;
using KafeshkaV2.Infrastructure;
using MySql.Data.MySqlClient;

namespace KafeshkaV2.DAL.implementations;
public class UserDal : IUserDal
{
    private readonly AppDbContext _dbContext;

    public UserDal(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<User> FindByEmail(string email)
    {
        // Используем EF Core для выполнения запросов
        return _dbContext.User.Where(u => u.email == email).ToList();
    }

    public User FindById(int id)
    {
        // Используем EF Core для выполнения запросов
        return _dbContext.User.FirstOrDefault(u => u.UserId == id);
    }
}