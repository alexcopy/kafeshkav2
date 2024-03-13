using System;
using KafeshkaV2.Controllers;
using KafeshkaV2.Models;
using KafeshkaV2.BL.interfaces;
using KafeshkaV2.DAL.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KafeshkaV2.Tests.Controllers;

public class UserControllerTests
{
    [Fact]
    public void User_Get_Returns_ViewResult()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<UserController>>();
        var userBlMock = new Mock<IUserBL>();
        var controller = new UserController(loggerMock.Object, userBlMock.Object);

        // Act
        var result = controller.User() as ViewResult;

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void User_Post_With_Valid_Model_Returns_ViewResult()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<UserController>>();
        var userBlMock = new Mock<IUserBL>();
        var controller = new UserController(loggerMock.Object, userBlMock.Object);
        var validModel = new LoginModelView { email = "test@example.com", password = "password" };

        userBlMock.Setup(b => b.Authenticate(validModel.email, validModel.password)).Returns(1);
        userBlMock.Setup(b => b.GetUserById(1)).Returns(new User());

        // Act
        var result = controller.User(validModel) as ViewResult;

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public void User_Post_With_Invalid_Model_Returns_ViewResult()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<UserController>>();
        var userBlMock = new Mock<IUserBL>();
        var controller = new UserController(loggerMock.Object, userBlMock.Object);
        var invalidModel = new LoginModelView { email = "test@example.com", password = "password" };

        userBlMock.Setup(b => b.Authenticate(invalidModel.email, invalidModel.password)).Returns((int?)null);

        // Act
        var result = controller.User(invalidModel) as ViewResult;

        // Assert
        Assert.NotNull(result);

    }
}