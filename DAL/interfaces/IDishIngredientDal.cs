using KafeshkaV2.DAL.Model;

namespace KafeshkaV2.DAL.interfaces;
 
    public interface IDishIngredientDal
    {
        DishIngredient GetById(int id);
        IEnumerable<DishIngredient> GetAll();
        DishIngredient FindById(int id);
        DishIngredient Create(DishIngredient dishIngredient);
        void Update(DishIngredient dishIngredient);
        void Delete(int id);
        
    }
 