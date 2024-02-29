using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.interfaces;
 
    public interface IDishIngredientDal
    {
        DishIngredient GetById(int id);
        IEnumerable<DishIngredient> GetAll();
        
    }
 