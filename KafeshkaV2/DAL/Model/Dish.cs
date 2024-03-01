namespace KafeshkaV2.DAL.Model;

 
public class Dish : IDish
{
    public int DishId { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Photo { get; set; } = string.Empty;
    public int Calories { get; set; }
    public bool Vegetarian { get; set; }
    public bool Allergy { get; set; }
    public string Notes { get; set; } = string.Empty;
    public ICollection<DishIngredient> DishIngredients { get; set; }
}