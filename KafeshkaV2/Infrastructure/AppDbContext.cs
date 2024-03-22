using KafeshkaV2.DAL.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
public class AppDbContext : IdentityDbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Dish> Dish { get; set; }
    public DbSet<DishIngredient> DishIngredients { get; set; }
    public DbSet<PaymentDetail> PaymentDetail { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure the User entity
        modelBuilder.Entity<User>(entity =>
                {
                    entity.HasKey(u => u.UserId);
                    entity.Property(u => u.email) .IsRequired();
                    entity.Property(u => u.FirstName) .IsRequired();
                    entity.Property(u => u.password) .IsRequired();
                });

        modelBuilder.Entity<PaymentDetail>(entity =>
        {
            entity.HasKey(k => k.PaymentDetailId);
            entity.Property(k => k.CardNumber).IsRequired();
            entity.Property(k => k.ExpirationDate).IsRequired();
            entity.Property(k => k.SecurityCode).IsRequired();
            entity.Property(k => k.CardOwnerName).IsRequired();
        });

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