using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.interfaces;

public interface IDishDal
{
    Dish? GetById(int id);
    IEnumerable<Dish> GetAll();

    Dish? Create(Dish dish);
    Dish? Update(Dish dish);
    void Delete(int id);
}