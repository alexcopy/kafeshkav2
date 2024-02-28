namespace KafeshkaV2.DAL.Model;

public interface IIngredient
{
    int IngredientId { get; set; }
    string Name { get; set; }
    string? Description { get; set; }
    string? Photo { get; set; }
    int Calories { get; set; }
    bool Allergy { get; set; }
    int Quantity { get; set; }
    string? Notes { get; set; }
}