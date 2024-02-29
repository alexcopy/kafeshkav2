using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.interfaces;

public interface IIngredientDal
{
    Ingredient GetById(int id);
    IEnumerable<Ingredient> GetAll();
    IEnumerable<Ingredient> FindByName(string name);
    Ingredient? Create(Ingredient ingredient);
    Ingredient? FindById(int id);
    void Update(Ingredient ingredient);
    void Delete(int id);
}