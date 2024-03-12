using System;

namespace KafeshkaV2.DAL.Model;
using Xunit;

public class DishTests
{
    [Fact]
    public void Dish_Name_Should_Not_Be_Null_Or_Empty()
    {
        // Arrange
        var dish = new Dish()
        {
            Name = "8asd8"
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => dish.Name="");
        Assert.Equal("Name should be at least 2 letters long.", exception.Message);
    }

    [Fact]
    public void Dish_Price_Should_Not_Be_Negative()
    {
        // Arrange
        var dish = new Dish()
        {
            Name = "Dish"
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => dish.Price = -1);
        Assert.Equal("Price should not be negative.", exception.Message);
    }

    [Fact]
    public void Dish_Calories_Should_Not_Be_Negative()
    {
        // Arrange
        var dish = new Dish()
        {
            Name = "Dish"
        };

        // Act & Assert
        var exception = Assert.Throws<ArgumentException>(() => dish.Calories = -1);
        Assert.Equal("Calories should not be negative.", exception.Message);
    }

    [Fact]
    public void Dish_Default_Values_Should_Be_Set_Correctly()
    {
        // Arrange & Act
        var dish = new Dish();
        // Assert
        Assert.Equal(0, dish.DishId);
        Assert.Equal(string.Empty, dish.Description);
        Assert.Equal(0.0m, dish.Price);
        Assert.Equal(string.Empty, dish.Photo);
        Assert.Equal(0, dish.Calories);
        Assert.False(dish.Vegetarian);
        Assert.False(dish.Allergy);
        Assert.Equal(string.Empty, dish.Notes);
        Assert.NotNull(dish.DishIngredients);
        Assert.Empty(dish.DishIngredients);
    }


}