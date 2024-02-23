using KafeshkaV2.DAL.Model;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configure your database connection here
        optionsBuilder.UseMySql("Server=localhost;Port=3386;Database=kafeshkav2;User ID=kafeshka;Password=test123;",
            new MySqlServerVersion(new Version(11, 2, 2)));
    }
}