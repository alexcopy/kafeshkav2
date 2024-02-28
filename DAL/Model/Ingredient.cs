namespace KafeshkaV2.DAL.Model;

public class Ingredient : IIngredient
{
    public int IngredientId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Photo { get; set; }
    public int Calories { get; set; }
    public bool Allergy { get; set; }
    public int Quantity { get; set; }
    public string? Notes { get; set; }
    // Навигационное свойство к DishIngredients
    public ICollection<DishIngredient> DishIngredients { get; set; }
}