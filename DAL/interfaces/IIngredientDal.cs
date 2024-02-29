using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.interfaces;

public interface IIngredientDal
{
    Ingredient GetById(int id);
    IEnumerable<Ingredient> GetAll();
    
}