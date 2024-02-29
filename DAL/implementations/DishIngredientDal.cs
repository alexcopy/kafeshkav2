using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.implementations;
public class DishIngredientDal : IDishIngredientDal
{
    private readonly AppDbContext _dbContext;

    public DishIngredientDal(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DishIngredient GetById(int id)
    {
        return _dbContext.DishIngredients.Find(id);
    }

    public IEnumerable<DishIngredient> GetAll()
    {
        return _dbContext.DishIngredients.ToList();
    }

    public DishIngredient FindById(int id)
    {
        // Implementation to find DishIngredient by id
        return _dbContext.DishIngredients.Find(id);
    }

    public DishIngredient Create(DishIngredient dishIngredient)
    {
        // Implementation to create a new DishIngredient
        _dbContext.DishIngredients.Add(dishIngredient);
        _dbContext.SaveChanges();
        return dishIngredient;
    }

    public void Update(DishIngredient dishIngredient)
    {
        // Implementation to update DishIngredient
        _dbContext.DishIngredients.Update(dishIngredient);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        // Implementation to delete DishIngredient with the specified id
        var dishIngredient = _dbContext.DishIngredients.Find(id);

        if (dishIngredient != null)
        {
            _dbContext.DishIngredients.Remove(dishIngredient);
            _dbContext.SaveChanges();
        }
        // Handle the case where dishIngredient is null if needed
    }
}