namespace KafeshkaV2.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;

 
[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientDal _ingredientDal;

    public IngredientsController(IIngredientDal ingredientDal)
    {
        _ingredientDal = ingredientDal;
    }

    [HttpGet]
    public IActionResult GetAllIngredients()
    {
        var ingredients = _ingredientDal.GetAll();
        return Ok(ingredients);
    }

    [HttpGet("{id}")]
    public IActionResult GetIngredientById(int id)
    {
        var ingredient = _ingredientDal.FindById(id);

        if (ingredient == null)
        {
            return NotFound();
        }

        return  Ok(ingredient);
    }

    [HttpGet("search")]
    public IActionResult GetIngredientsByName([FromQuery] string name)
    {
        var ingredients = _ingredientDal.FindByName(name);

        if (ingredients == null || !ingredients.Any())
        {
            return NotFound();
        }

        return  Ok(ingredients);
    }

    [HttpPost]
    public IActionResult CreateIngredient([FromBody] Ingredient ingredient)
    {
        if (ingredient == null)
        {
            return BadRequest();
        }

        var createdIngredient = _ingredientDal.Create(ingredient);

        return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.IngredientId }, createdIngredient);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateIngredient(int id, [FromBody] Ingredient ingredient)
    {
        if (ingredient == null || id != ingredient.IngredientId)
        {
            return BadRequest();
        }

        var existingIngredient = _ingredientDal.FindById(id);

        if (existingIngredient == null)
        {
            return NotFound();
        }

        _ingredientDal.Update(ingredient);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteIngredient(int id)
    {
        var existingIngredient = _ingredientDal.FindById(id);
        if (existingIngredient == null)
        {
            return NotFound();
        }
        _ingredientDal.Delete(id);
        return NoContent();
    }
}