using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TaskMaster.Controllers;
using TaskMaster.Domain.Services.Abstractions;
using Xunit;

namespace TaskMasterTest.Controllers
{
    public class CommentControllerTests
    {
        private MockRepository mockRepository;

        private Mock<ICommentService> mockCommentService;
        private Mock<IValidateUserService> mockValidateUserService;

        public CommentControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockCommentService = this.mockRepository.Create<ICommentService>();
            this.mockValidateUserService = this.mockRepository.Create<IValidateUserService>();
        }

        private CommentController CreateCommentController()
        {
            return new CommentController(
                this.mockCommentService.Object,
                this.mockValidateUserService.Object);
        }

        [Fact]
        public async Task GetAll_StateNullParameterTest()
        {
            // Arrange
            var commentController = this.CreateCommentController();
            string taskId = null;
            string userId = null;

            // Act
            var result = await commentController.GetAll(
                taskId,
                userId);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Get_StateNullParameterTest()
        {
            // Arrange
            var commentController = this.CreateCommentController();
            string id = null;
            string userId = null;

            // Act
            var result = await commentController.Get(
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
            var commentController = this.CreateCommentController();
            TaskMaster.Entities.DTOs.AddCommentDTO comment = null;

            // Act
            var result = await commentController.post(
                comment);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Update_StateNullParameterTest()
        {
            // Arrange
            var commentController = this.CreateCommentController();
            string userId = null;
            TaskMaster.Entities.Master.Comment commentData = null;

            // Act
            var result = await commentController.Update(
                userId,
                commentData);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }

        [Fact]
        public async Task Delete_StateNullParameterTest()
        {
            // Arrange
            var commentController = this.CreateCommentController();
            string id = null;
            string userId = null;

            // Act
            var result = await commentController.Delete(
                id,
                userId);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }
    }
}