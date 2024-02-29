                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.implementations;

public class DishDal : IDishDal
{
    private readonly AppDbContext _dbContext;

    public DishDal(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Dish? GetById(int id)
    {
        return _dbContext.Dish.Find(id);
    }

    public IEnumerable<Dish> GetAll()
    {
        return _dbContext.Dish.ToList();
    }

    public Dish Create(Dish dish)
    {
        _dbContext.Dish.Add(dish);
        _dbContext.SaveChanges();
        return dish;
    }

 
    public Dish Update(Dish dish)
    {
        _dbContext.Dish.Update(dish);
        _dbContext.SaveChanges();
        return dish;
    }

    public void Delete(int id)
    {
        var dishToDelete = _dbContext.Dish.Find(id);
        if (dishToDelete == null) return;
        _dbContext.Dish.Remove(dishToDelete);
        _dbContext.SaveChanges();
    }
    
}