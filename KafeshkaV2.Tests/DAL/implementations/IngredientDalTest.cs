using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.Model;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KafeshkaV2.Tests.DAL.implementations;

[TestSubject(typeof(IngredientDal))]
public class IngredientDalTest
{

    [Fact]
    public void GetById_Should_Return_Correct_Ingredient()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var ingredientDal = new IngredientDal(dbContext);

        var expectedIngredient = new Ingredient
        {
            Name = "Ingrident",
            // Set other properties as needed
        };

        dbContext.Ingredients.Add(expectedIngredient);
        dbContext.SaveChanges();

        // Act
        var result = ingredientDal.GetById(expectedIngredient.IngredientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedIngredient.IngredientId, result.IngredientId);
        // Assert other properties as needed
    }

    [Fact]
    public void GetById_Should_Return_Null_If_Ingredient_Not_Found()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var ingredientDal = new IngredientDal(dbContext);

        // Act
        var result = ingredientDal.GetById(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAll_Should_Return_All_Ingredients()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var ingredientDal = new IngredientDal(dbContext);
        var initialTable = ingredientDal.GetAll();
        var ingredients = new List<Ingredient>
        {
            new Ingredient { Name = "Ingrident1", },
            new Ingredient { Name = "Ingrident2", },

        };

        dbContext.Ingredients.AddRange(ingredients);
        dbContext.SaveChanges();

        // Act
        var result = ingredientDal.GetAll();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ingredients.Count()  + initialTable.Count(), result.Count());
        // Assert other properties or use collection assertions as needed
    }

    [Fact]
    public void FindById_Should_Return_Correct_Ingredient()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var ingredientDal = new IngredientDal(dbContext);

        var expectedIngredient = new Ingredient
        {
            Name = "Ingrident1",
            // Set other properties as needed
        };

        dbContext.Ingredients.Add(expectedIngredient);
        dbContext.SaveChanges();

        // Act
        var result = ingredientDal.FindById(expectedIngredient.IngredientId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedIngredient.IngredientId, result.IngredientId);
        // Assert other properties as needed
    }

    [Fact]
    public void FindByName_Should_Return_Correct_Ingredients()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var ingredientDal = new IngredientDal(dbContext);

        var ingredients = new List<Ingredient>
        {
            new Ingredient { Name = "Ingredient1" },
            new Ingredient { Name = "Ingredient2" },
            new Ingredient { Name = "Ingredient1" },
        };

        dbContext.Ingredients.AddRange(ingredients);
        dbContext.SaveChanges();

        // Act
        var result = ingredientDal.FindByName("Ingredient1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count()); // Two ingredients with the specified name
        // Assert other properties or use collection assertions as needed
    }

    [Fact]
    public void Delete_Should_Remove_Existing_Ingredient()
    {
        // Arrange
        using var dbContext = CreateDbContext();
        var ingredientDal = new IngredientDal(dbContext);

        var existingIngredient = new Ingredient
        {
            Name = "Ingredient_test1"

        };

        dbContext.Ingredients.Add(existingIngredient);
        dbContext.SaveChanges();

        // Act
        ingredientDal.Delete(existingIngredient.IngredientId);

        // Assert
        // Verify that the existing Ingredient was deleted from the database
        var deletedIngredient = dbContext.Ingredients.Find(existingIngredient.IngredientId);
        Assert.Null(deletedIngredient);
    }

    private static AppDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        return new AppDbContext(options);
    }
}