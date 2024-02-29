using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.implementations;

public class IngredientDal : IIngredientDal
{
    private readonly AppDbContext _dbContext;

    public IngredientDal(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Ingredient GetById(int id)
    {
        return _dbContext.Ingredients.Find(id);
    }

    public IEnumerable<Ingredient> GetAll()
    {
        return _dbContext.Ingredients.ToList();
    }
    
}