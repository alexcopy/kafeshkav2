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

     
}