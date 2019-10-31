using LeadDataAPI.Controllers;
using LeadDataAPI.Entities;
using LeadDataAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeadDataAPIUnitTest
{
    /// <summary>
    /// Test Cases for Users Controller
    /// </summary>
    public class UsersControllerTest
    {
        private readonly Mock<IUserService> _mockRepo;
        private readonly UsersController _controller;

        public UsersControllerTest()
        {
            _mockRepo = new Mock<IUserService>();
            _controller = new UsersController(_mockRepo.Object);
        }

        /// <summary>
        /// Test for Authenticate with valid username/password
        /// </summary>
        [Fact]
        public void Authenticate_ReturnsOkResult()
        {
            _mockRepo.Setup(repo => repo.Authenticate("test1", "test")).Returns(new User { Token = "12123hfgzxbcheoeopwer123nfjvnfjvfvvbifhbvvbivb" });
            var okResult = _controller.Authenticate(new User { Username = "test1", Password = "test" }) as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            var userObject = Assert.IsType<User>(okResult.Value);
            Assert.NotNull(userObject.Token);
        }


        /// <summary>
        /// Test for Authenticate with invalid username/password
        /// </summary>
        [Fact]
        public void Authenticate_ReturnsBadRequestResult()
        {
            var result = _controller.Authenticate(new User { Username = "test1", Password = "test" });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

        }
    }

}
