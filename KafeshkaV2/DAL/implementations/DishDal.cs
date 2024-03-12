                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            using Dapper;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.implementations;

public class DishDal(AppDbContext dbContext) : IDishDal
{
    public Dish? GetById(int id)
    {
        return dbContext.Dish.Find(id);
    }

    public IEnumerable<Dish> GetAll()
    {
        return dbContext.Dish.ToList();
    }

    public Dish Create(Dish dish)
    {
        dbContext.Dish.Add(dish);
        dbContext.SaveChanges();
        return dish;
    }

 
    public Dish Update(Dish dish)
    {
        dbContext.Dish.Update(dish);
        dbContext.SaveChanges();
        return dish;
    }

    public void Delete(int id)
    {
        var dishToDelete = dbContext.Dish.Find(id);
        if (dishToDelete == null) return;
        dbContext.Dish.Remove(dishToDelete);
        dbContext.SaveChanges();
    }

    public Dish FindById(int dishId)
    {
        return  dbContext.Dish.Find(dishId);
    }
}