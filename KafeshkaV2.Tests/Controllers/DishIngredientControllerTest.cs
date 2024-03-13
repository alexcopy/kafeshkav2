using JetBrains.Annotations;
using KafeshkaV2.Controllers;
using Xunit;
using System;
using System.Collections.Generic;
using KafeshkaV2.DAL.Model;
using KafeshkaV2.DAL.implementations;
using KafeshkaV2.DAL.interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace KafeshkaV2.Tests.Controllers;

[TestSubject(typeof(DishIngredientController))]
public class DishIngredientControllerTest
{
    [Fact]
    public void GetAll_Should_Return_All_Dishes()
    {
        // Arrange
        var mockDishDal = new Mock<IDishDal>();
        var controller = new DishController(mockDishDal.Object);

        var expectedDishes = new List<Dish>
        {
            new Dish { Name = "Dish1" },
            new Dish { Name = "Dish2" },

        };

        mockDishDal.Setup(d => d.GetAll()).Returns(expectedDishes);

        // Act
        var result = controller.GetAll() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDishes, result.Value);
    }

    [Fact]
    public void GetById_Should_Return_Correct_Dish()
    {
        // Arrange
        var mockDishDal = new Mock<IDishDal>();
        var controller = new DishController(mockDishDal.Object);

        var expectedDish = new Dish { DishId = 1, Name = "Dish1" };

        mockDishDal.Setup(d => d.GetById(expectedDish.DishId)).Returns(expectedDish);

        // Act
        var result = controller.GetById(expectedDish.DishId) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedDish, result.Value);
    }

    [Fact]
    public void GetById_Should_Return_NotFound_When_Dish_Not_Found()
    {
        // Arrange
        var mockDishDal = new Mock<IDishDal>();
        var controller = new DishController(mockDishDal.Object);

        mockDishDal.Setup(d => d.GetById(It.IsAny<int>())).Returns((Dish)null);

        // Act
        var result = controller.GetById(999) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

[Fact]
    public void Create_Should_Return_CreatedAtAction_When_Successful()
    {
        // Arrange
        var mockDishDal = new Mock<IDishDal>();
        var controller = new DishController(mockDishDal.Object);

        var newDish = new Dish { Name = "New Dish" };

        mockDishDal.Setup(d => d.Create(It.IsAny<Dish>())).Returns(newDish);

        // Act
        var result = controller.Create(newDish) as CreatedAtActionResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(nameof(DishController.GetById), result.ActionName);
        Assert.Equal(newDish.DishId, result.RouteValues["id"]);
        Assert.Equal(newDish, result.Value);
    }

    [Fact]
    public void Create_Should_Return_BadRequest_When_Model_Invalid()
    {
        // Arrange
        var mockDishDal = new Mock<IDishDal>();
        var controller = new DishController(mockDishDal.Object);

        controller.ModelState.AddModelError("Name", "Name is required");

        // Act
        var result = controller.Create(new Dish()) as BadRequestResult;

        // Assert
        Assert.Null(result);

    }

    [Fact]
    public void Update_Should_Return_Ok_When_Successful()
    {
        // Arrange
        var mockDishDal = new Mock<IDishDal>();
        var controller = new DishController(mockDishDal.Object);

        var existingDishId = 1;
        var existingDish = new Dish { DishId = existingDishId, Name = "Existing Dish" };
        var updatedDish = new Dish { DishId = existingDishId, Name = "Updated Dish" };

        mockDishDal.Setup(d => d.GetById(existingDishId)).Returns(existingDish);
        mockDishDal.Setup(d => d.Update(It.IsAny<Dish>())).Returns(updatedDish);

        // Act
        var result = controller.Update(existingDishId, updatedDish) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedDish, result.Value);
    }

    [Fact]
    public void Delete_Should_Return_NoContent_When_Successful()
    {
        // Arrange
        var mockDishDal = new Mock<IDishDal>();
        var controller = new DishController(mockDishDal.Object);

        var existingDishId = 1;
        var existingDish = new Dish { DishId = existingDishId, Name = "Existing Dish" };

        mockDishDal.Setup(d => d.GetById(existingDishId)).Returns(existingDish);

        // Act
        var result = controller.Delete(existingDishId) as NoContentResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(204, result.StatusCode);
    }

    [Fact]
    public void Delete_Should_Return_NotFound_When_Dish_Not_Found()
    {
        // Arrange
        var mockDishDal = new Mock<IDishDal>();
        var controller = new DishController(mockDishDal.Object);

        mockDishDal.Setup(d => d.GetById(It.IsAny<int>())).Returns((Dish)null);

        // Act
        var result = controller.Delete(999) as NotFoundResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(404, result.StatusCode);
    }

    private static DishController CreateDishControllerWithMockDal(Mock<IDishDal> mockDishDal)
    {
        return new DishController(mockDishDal.Object);
    }

}