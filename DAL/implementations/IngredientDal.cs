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

    public Ingredient FindById(int id)
    {
        return _dbContext.Ingredients.Find(id);
    }

    public Ingredient GetById(int id)
    {
        return _dbContext.Ingredients.Find(id);
    }

    public IEnumerable<Ingredient> GetAll()
    {
        // Implementation to get all ingredients
        return _dbContext.Ingredients.ToList();
    }

    public IEnumerable<Ingredient> FindByName(string name)
    {
        // Implementation to find ingredients by name
        return _dbContext.Ingredients.Where(i => i.Name == name).ToList();
    }

    public Ingredient Create(Ingredient ingredient)
    {
        _dbContext.Ingredients.Add(ingredient);
        _dbContext.SaveChanges();
        return ingredient;
    }

    public void Update(Ingredient ingredient)
    {
        _dbContext.Ingredients.Update(ingredient);
        _dbContext.SaveChanges();
    }

    public void Delete(int id)
    {
        var ingredient = _dbContext.Ingredients.Find(id);
        if (ingredient != null)
        {
            _dbContext.Ingredients.Remove(ingredient);
            _dbContext.SaveChanges();
        }
    }
}