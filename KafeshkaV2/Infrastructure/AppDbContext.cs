using KafeshkaV2.DAL.Model;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Dish> Dish { get; set; }
    public DbSet<DishIngredient> DishIngredients { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the User entity
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId); // Assuming UserId is the primary key

        modelBuilder.Entity<User>()
            .Property(u => u.email)
            .IsRequired();

        modelBuilder.Entity<DishIngredient>()
            .HasKey(di => di.DishIngredientId);

        modelBuilder.Entity<DishIngredient>()
            .HasOne(di => di.Dish)
            .WithMany(d => d.DishIngredients)
            .HasForeignKey(di => di.DishId);

        modelBuilder.Entity<DishIngredient>()
            .HasOne(di => di.Ingredient)
            .WithMany(i => i.DishIngredients)
            .HasForeignKey(di => di.IngredientId);
    }
}