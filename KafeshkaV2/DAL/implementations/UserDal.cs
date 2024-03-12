using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;
using Microsoft.EntityFrameworkCore;

namespace KafeshkaV2.DAL.implementations;

public class UserDal(AppDbContext dbContext) : IUserDal
{
    public IEnumerable<User> FindByEmail(string email)
    {
        // Используем EF Core для выполнения запросов
        return dbContext.User.Where(u => u.email == email).ToList();
    }

    public User FindById(int id)
    {
        // Используем EF Core для выполнения запросов
        return dbContext.User.FirstOrDefault(u => u.UserId == id);
    }

    public void AddUser(User user)
    {
        // Ensure the email is unique before attempting to add the user
        if (dbContext.User.Any(u => u.email == user.email))
        {
            throw new DbUpdateException("User with the same email already exists.");
        }

        // Hash the password (replace this with your actual password hashing logic)
        user.password = HashPassword(user.password);

        // Add the user to the database
        dbContext.User.Add(user);
        dbContext.SaveChanges();
    }

    // Placeholder for password hashing logic (replace with your actual implementation)
    private string HashPassword(string password)
    {
        // Replace this with your password hashing logic (e.g., using bcrypt or another secure hashing algorithm)
        return password;
    }
}