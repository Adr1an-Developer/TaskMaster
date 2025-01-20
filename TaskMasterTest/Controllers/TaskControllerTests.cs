using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TaskMaster.Controllers;
using TaskMaster.Domain.Services.Abstractions;
using Xunit;

namespace TaskMasterTest.Controllers
{
    public class TaskControllerTests
    {
        private MockRepository mockRepository;

        private Mock<ITaskService> mockTaskService;
        private Mock<IValidateUserService> mockValidateUserService;

        public TaskControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockTaskService = this.mockRepository.Create<ITaskService>();
            this.mockValidateUserService = this.mockRepository.Create<IValidateUserService>();
        }

        private TaskController CreateTaskController()
        {
            return new TaskController(
                this.mockTaskService.Object,
                this.mockValidateUserService.Object);
        }

        [Fact]
        public async Task GetAll_StateNullParameterTest()
        {
            // Arrange
            var taskController = this.CreateTaskController();
            string projectId = null;
            string userId = null;

            // Act
            var result = await taskController.GetAll(
                projectId,
                userId);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Get_StateNullParameterTest()
        {
            // Arrange
            var taskController = this.CreateTaskController();
            string id = null;
            string userId = null;

            // Act
            var result = await taskController.Get(
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
            var taskController = this.CreateTaskController();
            TaskMaster.Entities.DTOs.AddTaskDTO task = null;

            // Act
            var result = await taskController.post(
                task);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Update_StateNullParameterTest()
        {
            // Arrange
            var taskController = this.CreateTaskController();
            string userId = null;
            TaskMaster.Entities.Master.Task taskData = null;

            // Act
            var result = await taskController.Update(
                userId,
                taskData);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_StateNullParameterTest()
        {
            // Arrange
            var taskController = this.CreateTaskController();
            string id = null;
            string userId = null;

            // Act
            var result = await taskController.Delete(
                id,
                userId);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }
    }
}