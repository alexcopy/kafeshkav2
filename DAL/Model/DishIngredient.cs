namespace KafeshkaV2.DAL.Model;

public class DishIngredient : IDishIngredient
{
    public int DishIngredientId { get; set; }
    public int DishId { get; set; }
    public int IngredientId { get; set; }
    public int Quantity { get; set; }

    // Навигационные свойства
    public required Dish Dish { get; set; }
    public required Ingredient Ingredient { get; set; }
}