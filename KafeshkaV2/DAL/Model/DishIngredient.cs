namespace KafeshkaV2.DAL.Model;

public class DishIngredient : IDishIngredient
{
    private int _quantity;

    public int DishIngredientId { get; set; }
    public int DishId { get; set; }
    public int IngredientId { get; set; }

    public int Quantity
    {
        get => _quantity;
        set
        {
            if (value < 0)
            {
                throw new InvalidOperationException("Quantity should not be negative.");
            }
            _quantity = value;
        }
    }

    public void SetQuantity(int value)
    {
        throw new NotImplementedException();
    }

    // Overloaded set method
    public void SetDishIngredient(int dishId, int ingredientId, int quantity)
    {
        DishId = dishId;
        IngredientId = ingredientId;
        Quantity = quantity;
    }

    // Навигационные свойства
    public required Dish Dish { get; set; }
    public required Ingredient Ingredient { get; set; }

    // Existing constructor
    public DishIngredient() { }
}