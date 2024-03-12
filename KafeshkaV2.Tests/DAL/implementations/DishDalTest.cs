using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KafeshkaV2.Tests.DAL.implementations;

[TestSubject(typeof(DishDal))]
public class DishDalTest
{
    private readonly SqliteConnection _connection;
    private readonly DbContextOptions<AppDbContext> _options;

    [Fact]
    public void GetById_Should_Return_Correct_Dish()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishDal = new DishDal(dbContext);

        var expectedDish = new Dish
        {
            Name = "Dish 1"
        };

        dbContext.Dish.Add(expectedDish);
        dbContext.SaveChanges();

        // Act
        var result = dishDal.GetById(expectedDish.DishId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDish.DishId, result.DishId);
    }

    [Fact]
    public void GetById_Should_Return_Null_If_Dish_Not_Found()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishDal = new DishDal(dbContext);

        // Act
        var result = dishDal.GetById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_Should_Return_All_Dishes()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishDal = new DishDal(dbContext);
        var initialTable = dishDal.GetAll();

        var dishes = new List<Dish>
        {
            new Dish { Name = "Dish 1", },
            new Dish { Name = "Dish 2", },
        };
        dbContext.Dish.AddRange(dishes);
        dbContext.SaveChanges();

        // Act
        var result = dishDal.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dishes.Count + initialTable.Count() + 2 , result.Count());
    }

    [Fact]
    public void FindById_Should_Return_Correct_Dish()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishDal = new DishDal(dbContext);

        var expectedDish = new Dish
        {
            Name = "Dish Name",
        };

        dbContext.Dish.Add(expectedDish);
        dbContext.SaveChanges();

        // Act
        var result = dishDal.FindById(expectedDish.DishId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDish.DishId, result.DishId);
        // Assert other properties as needed
    }

    [Fact]
    public void Delete_Should_Remove_Existing_Dish()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var dishDal = new DishDal(dbContext);

        var existingDish = new Dish
        {
            Name = "Dish Name",
        };

        dbContext.Dish.Add(existingDish);
        dbContext.SaveChanges();

        // Act
        dishDal.Delete(existingDish.DishId);

        // Assert
        // Verify that the existing Dish was deleted from the database
        var deletedDish = dbContext.Dish.Find(existingDish.DishId);
        Assert.Null(deletedDish);
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return new AppDbContext(options);
    }

    public void Dispose()
    {
        _connection.Close();
    }
}