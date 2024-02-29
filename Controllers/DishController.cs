using System.Diagnostics;
using KafeshkaV2.DAL.interfaces;
using KafeshkaV2.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using KafeshkaV2.Models;

namespace KafeshkaV2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DishController : ControllerBase
{
    private readonly IDishDal _dishDal;

    public DishController(IDishDal dishDal)
    {
        _dishDal = dishDal;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var dishes = _dishDal.GetAll();
        return Ok(dishes);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var dish = _dishDal.GetById(id);
        if (dish == null)
            return NotFound();

        return Ok(dish);
    }
    [HttpPost]
    public IActionResult Create([FromBody] Dish dish)
    {
        if (dish == null)
            return BadRequest();

        var createdDish = _dishDal.Create(dish);

        return createdDish == null ? StatusCode(500, "Failed to create dish") : // or handle the null case appropriately
            CreatedAtAction(nameof(GetById), new { id = createdDish.DishId }, createdDish);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Dish dish)
    {
        if (dish == null || id != dish.DishId)
            return BadRequest();

        var existingDish = _dishDal.GetById(id);
        if (existingDish == null)
            return NotFound();

        var updatedDish = _dishDal.Update(dish);
        return Ok(updatedDish);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var existingDish = _dishDal.GetById(id);
        if (existingDish == null)
            return NotFound();

        _dishDal.Delete(id);
        return NoContent();
    }
     
}