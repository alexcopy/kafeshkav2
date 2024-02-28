namespace KafeshkaV2.DAL.Model;

public interface IDishIngredient
{
    int DishIngredientId { get; set; }
    int DishId { get; set; }
    int IngredientId { get; set; }
    int Quantity { get; set; }
}
