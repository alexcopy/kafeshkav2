namespace KafeshkaV2.DAL.Model;
using Xunit;

public class IngredientTests
{
    private DishIngredient CreateDishIngredientWithIngredient()
    {
        var ingredient = new Ingredient
        {
            Name = "Ingredient"
        };

        return new DishIngredient
        {
            Dish = new Dish(),
            Ingredient = ingredient
        };
    }

    [Fact]
    public void DishIngredients_Should_Be_Empty_After_Construction()
    {
        // Arrange
        var ingredient = new Ingredient
        {
            Name = "Name"
        };
        var dishIngredient = CreateDishIngredientWithIngredient();
        // Act
        var dishIngredients = ingredient.DishIngredients;

        // Assert
        Assert.NotNull(dishIngredients);
        Assert.Empty(dishIngredients);
    }

    [Fact]
    public void AddDishIngredient_Should_Add_Element_To_DishIngredients()
    {
        var ingredient = new Ingredient
        {
            Name = "Name"
        };
        var dishIngredient = CreateDishIngredientWithIngredient();

        // Act
        ingredient.DishIngredients.Add(dishIngredient);

        // Assert
        Assert.Single(ingredient.DishIngredients);
        Assert.Contains(dishIngredient, ingredient.DishIngredients);
    }

    [Fact]
    public void RemoveDishIngredient_Should_Remove_Element_From_DishIngredients()
    {
        var ingredient = new Ingredient
        {
            Name = "Name"
        };
        var dishIngredient = CreateDishIngredientWithIngredient();
        ingredient.DishIngredients.Add(dishIngredient);

        // Act
        ingredient.DishIngredients.Remove(dishIngredient);

        // Assert
        Assert.Empty(ingredient.DishIngredients);
    }
}