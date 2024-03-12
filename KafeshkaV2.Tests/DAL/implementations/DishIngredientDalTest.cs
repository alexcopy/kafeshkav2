using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KafeshkaV2.Tests.DAL.implementations;

[TestSubject(typeof(DishIngredientDal))]
public class DishIngredientDalTests
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AppDbContext> _options;


    public DishIngredientDalTests()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        using var dbContext = new AppDbContext(_options);
        dbContext.Database.EnsureCreated();
    }

    public void Dispose()
    {
        _connection.Close();
    }

    [Fact]
    public void GetById_Should_Return_Correct_DishIngredient()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishIngredientDal = new DishIngredientDal(dbContext);

        var expectedDishIngredient = getDishIngredient();

        dbContext.DishIngredients.Add(expectedDishIngredient);
        dbContext.SaveChanges();

        // Act
        var result = dishIngredientDal.GetById(expectedDishIngredient.DishIngredientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDishIngredient.DishIngredientId, result.DishIngredientId);
        // Assert other properties as needed
    }

    private static DishIngredient getDishIngredient()
    {
        var dishIngredient = new DishIngredient
        {
            Dish = new Dish()
            {
                Name = "Dish"
            },
            Ingredient = new Ingredient()
            {
                Name = "Ingredient Name"
            },
        };
        return dishIngredient;
    }

    [Fact]
    public void GetById_Should_Return_Null_If_DishIngredient_Not_Found()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishIngredientDal = new DishIngredientDal(dbContext);

        // Act
        var result = dishIngredientDal.GetById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_Should_Return_All_DishIngredients()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishIngredientDal = new DishIngredientDal(dbContext);

        var dishIngredients = new List<DishIngredient>
        {
            getDishIngredient(),
            getDishIngredient(),
            // Add more DishIngredients as needed
        };

        dbContext.DishIngredients.AddRange(dishIngredients);
        dbContext.SaveChanges();

        // Act
        var result = dishIngredientDal.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dishIngredients.Count, result.Count());
        // Assert other properties or use collection assertions as needed
    }

    [Fact]
    public void FindById_Should_Return_Correct_DishIngredient()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishIngredientDal = new DishIngredientDal(dbContext);

        var expectedDishIngredient = getDishIngredient();

        dbContext.DishIngredients.Add(expectedDishIngredient);
        dbContext.SaveChanges();

        // Act
        var result = dishIngredientDal.FindById(expectedDishIngredient.DishIngredientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDishIngredient.DishIngredientId, result.DishIngredientId);
        // Assert other properties as needed
    }

    // Add similar tests for Create, Update, and Delete methods as needed
    // ...

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public void Create_Should_Add_New_DishIngredient()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishIngredientDal = new DishIngredientDal(dbContext);

        var newDishIngredient = new DishIngredient
        {
            Dish = new Dish()
            {
                Name = "Dish"
            },
            Ingredient = new Ingredient()
            {
                Name = "Ingredient"
            },
        };

        // Act
        var result = dishIngredientDal.Create(newDishIngredient);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newDishIngredient.DishIngredientId, result.DishIngredientId);
        // Assert other properties as needed

        // Verify that the DishIngredient was added to the database
        var addedDishIngredient = dbContext.DishIngredients.Find(result.DishIngredientId);
        Assert.NotNull(addedDishIngredient);
        // Assert other properties as needed
    }

    [Fact]
    public void Update_Should_Modify_Existing_DishIngredient()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishIngredientDal = new DishIngredientDal(dbContext);

        var existingDishIngredient = getDishIngredient();

        dbContext.DishIngredients.Add(existingDishIngredient);
        dbContext.SaveChanges();



        var updatedDishName = "Updated Dish Name";

        // Ensure that the DishIngredient is tracked by the context
        dbContext.Entry(existingDishIngredient).State = EntityState.Detached;

        // Modify the Dish name
        existingDishIngredient.Dish.Name = updatedDishName;

        // Act
        dishIngredientDal.Update(existingDishIngredient);


        // Assert
        // Verify that the existing DishIngredient was updated in the database
        var modifiedDishIngredient = dbContext.DishIngredients.Find(existingDishIngredient.DishIngredientId);
        Assert.NotNull(modifiedDishIngredient);
        Assert.Equal(updatedDishName, modifiedDishIngredient.Dish.Name);
    }

    [Fact]
    public void Delete_Should_Remove_Existing_DishIngredient()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishIngredientDal = new DishIngredientDal(dbContext);

        var existingDishIngredient = getDishIngredient();
        dbContext.DishIngredients.Add(existingDishIngredient);
        dbContext.SaveChanges();
        // Act
        dishIngredientDal.Delete(existingDishIngredient.DishIngredientId);
        // Assert
        // Verify that the existing DishIngredient was deleted from the database
        var deletedDishIngredient = dbContext.DishIngredients.Find(existingDishIngredient.DishIngredientId);
        Assert.Null(deletedDishIngredient);
    }
}