namespace KafeshkaV2.Controllers;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;

[ApiController]
[Route("api/[controller]")]
public class DishIngredientController : ControllerBase
{
    private readonly IDishIngredientDal _dishIngredientDal;

    public DishIngredientController(IDishIngredientDal dishIngredientDal)
    {
        _dishIngredientDal = dishIngredientDal;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DishIngredient>> GetDishIngredients()
    {
        var dishIngredients = _dishIngredientDal.GetAll();
        return Ok(dishIngredients);
    }

    [HttpGet("{id}")]
    public ActionResult<DishIngredient> GetDishIngredientById(int id)
    {
        var dishIngredient = _dishIngredientDal.FindById(id);

        if (dishIngredient == null)
        {
            return NotFound();
        }

        return Ok(dishIngredient);
    }

    [HttpPost]
    public ActionResult<DishIngredient> CreateDishIngredient(DishIngredient dishIngredient)
    {
        var createdDishIngredient = _dishIngredientDal.Create(dishIngredient);
        return CreatedAtAction(nameof(GetDishIngredientById), new { id = createdDishIngredient.DishIngredientId }, createdDishIngredient);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDishIngredient(int id, DishIngredient dishIngredient)
    {
        if (id != dishIngredient.DishIngredientId)
        {
            return BadRequest();
        }

        _dishIngredientDal.Update(dishIngredient);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteDishIngredient(int id)
    {
        var dishIngredient = _dishIngredientDal.FindById(id);

        if (dishIngredient == null)
        {
            return NotFound();
        }

        _dishIngredientDal.Delete(id);

        return NoContent();
    }
}
