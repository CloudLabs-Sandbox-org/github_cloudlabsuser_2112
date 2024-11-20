using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;
using Xunit;

namespace MyMvcApp.Tests.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public void Details_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var controller = new UserController();
            int nonExistentUserId = 999;

            // Act
            var result = controller.Details(nonExistentUserId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Details_ReturnsViewResult_WithUser()
        {
            // Arrange
            var controller = new UserController();
            var user = new User { Id = 1, Name = "Test User", Email = "test@example.com" };
            UserController.userlist.Add(user);

            // Act
            var result = controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal(user.Id, model.Id);
            Assert.Equal(user.Name, model.Name);
            Assert.Equal(user.Email, model.Email);

            // Cleanup
            UserController.userlist.Clear();
        }
    }
}