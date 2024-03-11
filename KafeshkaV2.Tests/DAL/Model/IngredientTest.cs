using System;
using KafeshkaV2.DAL.Model;


namespace KafeshkaV2.Tests.DAL.Model;

using Xunit;

public class DishIngredientTests
{
    private Dish CreateDefaultDish()
    {
        return new Dish
        {
            Name = "Default Dish Name"
        };
    }

    private Ingredient CreateDefaultIngredient()
    {
        return new Ingredient
        {
            Name = "Default Ingredient Name"
        };
    }

    private DishIngredient CreateDefaultDishIngredient()
    {
        var dish = CreateDefaultDish();
        var ingredient = CreateDefaultIngredient();

        return new DishIngredient
        {
            Dish = dish,
            Ingredient = ingredient
        };
    }

    [Fact]
    public void DishIngredient_Navigation_Properties_Should_Not_Be_Null()
    {
        // Arrange
        var dishIngredient = CreateDefaultDishIngredient();

        // Act
        var dishProperty = dishIngredient.Dish;
        var ingredientProperty = dishIngredient.Ingredient;

        // Assert
        Assert.NotNull(dishProperty);
        Assert.NotNull(ingredientProperty);
    }

    [Fact]
    public void DishIngredient_Quantity_Should_Not_Be_Negative()
    {
        var dishIngredient = CreateDefaultDishIngredient();
        var exception = Assert.Throws<InvalidOperationException>(() => dishIngredient.Quantity = -1);
        Assert.Equal("Quantity should not be negative.", exception.Message);
    }


    [Fact]
    public void PropertiesShouldBeMappedCorrectly()
    {
        // Arrange
        var dishId = 1;
        var ingredientId = 2;
        var quantity = 3;
        var dishIngredient = CreateDefaultDishIngredient();
        dishIngredient.SetDishIngredient(dishId, ingredientId, quantity);
        // Assert
        Assert.Equal(dishId, dishIngredient.DishId);
        Assert.Equal(ingredientId, dishIngredient.IngredientId);
        Assert.Equal(quantity, dishIngredient.Quantity);
    }


    [Fact]
    public void DishIngredient_SetDishIngredient_Should_Throw_Exception_If_Quantity_Negative()
    {
        var dishIngredient = CreateDefaultDishIngredient();
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => dishIngredient.SetDishIngredient(1, 2, -1));
        Assert.Equal("Quantity should not be negative.", exception.Message);
    }

    [Fact]
    public void DishIngredient_SetDishIngredient_Should_Not_Throw_Exception_If_Quantity_Zero()
    {
        var dishIngredient = CreateDefaultDishIngredient();
        // Act
        dishIngredient.SetDishIngredient(1, 2, 0);

        // Assert
        Assert.Equal(0, dishIngredient.Quantity);
    }
    [Fact]
    public void DishIngredient_Default_Constructor_Should_Create_Object_With_Default_Values()
    {
        var dishIngredient = CreateDefaultDishIngredient();
        // Assert
        Assert.Equal(0, dishIngredient.DishId);
        Assert.Equal(0, dishIngredient.IngredientId);
        Assert.Equal(0, dishIngredient.Quantity);
        Assert.NotNull(dishIngredient.Dish);
        Assert.NotNull(dishIngredient.Ingredient);
    }
}