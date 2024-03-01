namespace KafeshkaV2.DAL.Model;

public interface IDish
{
    int DishId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
    string Photo { get; set; }
    int Calories { get; set; }
    bool Vegetarian { get; set; }
    bool Allergy { get; set; }
    string Notes { get; set; }
}