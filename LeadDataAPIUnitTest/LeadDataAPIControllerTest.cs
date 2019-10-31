using LeadDataAPI.Controllers;
using LeadDataAPI.DataAccess;
using LeadDataAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace LeadDataAPIUnitTest
{
    /// <summary>
    /// Test Cases for Lead Controller
    /// </summary>
    public class LeadControllerTest
    {
        private readonly Mock<IGenericRepository<Lead>> _mockRepo;
        private readonly LeadController _controller;

        public LeadControllerTest()
        {
            _mockRepo = new Mock<IGenericRepository<Lead>>();
            _controller = new LeadController(_mockRepo.Object);
        }

        /// <summary>
        /// Test for Get() method return type
        /// </summary>
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var okResult = _controller.Get();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        /// <summary>
        /// Test for Get() method return type and data
        /// </summary>
        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            //Add some data for testing
            _mockRepo.Setup(repo => repo.GetAll()).Returns(GetMockLeads());

            var okResult = _controller.Get() as OkObjectResult;

            // Assert
            var items = Assert.IsType<List<Lead>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }

        /// <summary>
        /// Test for GetByID() method return type and data
        /// </summary>
        [Fact]
        public void GetByID_WhenCalled_ReturnsSpecific()
        {
            //Add some data for testing
            int id = 1;
            _mockRepo.Setup(repo => repo.GetById(id))
        .Returns(GetMockLeads().FirstOrDefault(
            s => s.LeadId == id));
            
            var okResult = _controller.Get(1) as OkObjectResult;

            // Assert
            var item = Assert.IsType<Lead>(okResult.Value);
            Assert.Equal("Test1", item.FirstName);
        }

        /// <summary>
        /// Test for Post (Valid Object)
        /// </summary>
        [Fact]
        public void Insert_ValidObjectPassed_ReturnsCreatedResponse()
        {
            var leadItem = new Lead { LeadId = 1, FirstName = "Test1", LastName="Test", Email = "a@a.com", Company = "c1", AcceptTerms=true };

            var createdResponse = _controller.Post(leadItem);

            // Assert
            Assert.IsType<CreatedResult>(createdResponse);
        }

        /// <summary>
        /// Test for Post (Invalid email)
        /// </summary>
        [Fact]
        public void Insert_InvalidEmail_ReturnsBadRequest()
        {
            // invalid email
            var leadItem = new Lead { LeadId = 1, FirstName = "Test1", LastName="Test", Email = "a.com", Company = "c1", AcceptTerms = true };

            var createdResponse = _controller.Post(leadItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(createdResponse);
        }

        /// <summary>
        /// Test for Post (Null First Name)
        /// </summary>
        [Fact]
        public void Insert_NullFirstName_ReturnsBadRequest()
        {
            var leadItem = new Lead { LeadId = 1, FirstName = null, LastName = "Test", Email = "a.com", Company = "c1", AcceptTerms = true };

            var createdResponse = _controller.Post(leadItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(createdResponse);
        }

        /// <summary>
        /// Test for Post (Empty Last Name)
        /// </summary>
        [Fact]
        public void Insert_EmptyLastName_ReturnsBadRequest()
        {
            var leadItem = new Lead { LeadId = 1, FirstName = "Test", LastName = "", Email = "a.com", Company = "c1", AcceptTerms = true };

            var createdResponse = _controller.Post(leadItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(createdResponse);
        }

        /// <summary>
        /// Test for Post (Null Email)
        /// </summary>
        [Fact]
        public void Insert_NullEmail_ReturnsBadRequest()
        {
            var leadItem = new Lead { LeadId = 1, FirstName = "test", LastName = "test", Email = null, Company = "c1", AcceptTerms = true };

            var createdResponse = _controller.Post(leadItem);

            // Assert
            Assert.IsType<BadRequestObjectResult>(createdResponse);
        }

        /// <summary>
        /// Test for Delete return type
        /// </summary>
        [Fact]
        public void Delete_WhenCalled_RemovesSpecificItem()
        {

            var okResult = _controller.Delete(1);

            var item = Assert.IsType<OkResult>(okResult);
        }


        /// <summary>
        /// Test for Put return type
        /// </summary>
        [Fact]
        public void Put_WhenCalled_ReturnsOk()
        {

            var okResult = _controller.Put(1, new Lead { LeadId = 1, FirstName = "Test1", LastName="Test2", Email = "a@a.com", Company = "c1", AcceptTerms=true });

            var item = Assert.IsType<OkResult>(okResult);
        }

        /// <summary>
        /// Return sample Lead Data
        /// </summary>
        /// <returns>List of Lead Data</returns>
        public List<Lead> GetMockLeads()
        {
            return new List<Lead>
            {
                new Lead{ LeadId = 1, FirstName = "Test1" , Email = "a@a.com", Company = "c1" },
                new Lead{ LeadId = 2, FirstName = "Test2" , Email = "a@b.com", Company = "c1" },
                new Lead{ LeadId = 3, FirstName = "Test3" , Email = "a@c.com", Company = "c1" }
            };
        }
    }
}
