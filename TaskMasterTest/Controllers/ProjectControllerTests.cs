using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TaskMaster.Controllers;
using TaskMaster.Domain.Services.Abstractions;
using Xunit;

namespace TaskMasterTest.Controllers
{
    public class ProjectControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IProjectService> mockProjectService;
        private Mock<IValidateUserService> mockValidateUserService;

        public ProjectControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockProjectService = this.mockRepository.Create<IProjectService>();
            this.mockValidateUserService = this.mockRepository.Create<IValidateUserService>();
        }

        private ProjectController CreateProjectController()
        {
            return new ProjectController(
                this.mockProjectService.Object,
                this.mockValidateUserService.Object);
        }

        [Fact]
        public async Task GetAll_StateNullParameterTest()
        {
            // Arrange
            var projectController = this.CreateProjectController();
            string userId = null;

            // Act
            var result = await projectController.GetAll(
                userId);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Get_StateNullParameterTest()
        {
            // Arrange
            var projectController = this.CreateProjectController();
            string id = null;
            string userId = null;

            // Act
            var result = await projectController.Get(
                id,
                userId);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task post_StateNullParameterTest()
        {
            // Arrange
            var projectController = this.CreateProjectController();
            TaskMaster.Entities.DTOs.AddProjectDTO project = null;

            // Act
            var result = await projectController.post(
                project);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Update_StateNullParameterTest()
        {
            // Arrange
            var projectController = this.CreateProjectController();
            string userId = null;
            TaskMaster.Entities.Master.Project projectdata = null;

            // Act
            var result = await projectController.Update(
                userId,
                projectdata);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_StateNullParameterTest()
        {
            // Arrange
            var projectController = this.CreateProjectController();
            string id = null;
            string userId = null;

            // Act
            var result = await projectController.Delete(
                id,
                userId);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }
    }
}