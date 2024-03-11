namespace KafeshkaV2.DAL.Model;

public class Dish : IDish
{
    private string _name;
    public int DishId { get; set; }
    private decimal _price;
    private int _calories;

    public string Name
    {
        get => _name;
        set
        {
            if (value.Length < 2)
            {
                throw new ArgumentException("Name should be at least 2 letters long.");
            }
            _name = value;
        }
    }
    public decimal Price
    {
        get => _price;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Price should not be negative.");
            }

            _price = value;
        }
    }

    public int Calories
    {
        get => _calories;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Calories should not be negative.");
            }

            _calories = value;
        }
    }

    public string Description { get; set; } = string.Empty;
    public string Photo { get; set; } = string.Empty;
    public bool Vegetarian { get; set; }
    public bool Allergy { get; set; }
    public string Notes { get; set; } = string.Empty;
    public ICollection<DishIngredient> DishIngredients { get; set; } = new List<DishIngredient>();
}