using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TaskMaster.Controllers;
using TaskMaster.Domain.Services.Abstractions;
using Xunit;

namespace TaskMasterTest.Controllers
{
    public class ReportsControllerTests
    {
        private MockRepository mockRepository;

        private Mock<IReportsService> mockReportsService;
        private Mock<IValidateUserService> mockValidateUserService;

        public ReportsControllerTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);

            this.mockReportsService = this.mockRepository.Create<IReportsService>();
            this.mockValidateUserService = this.mockRepository.Create<IValidateUserService>();
        }

        private ReportsController CreateReportsController()
        {
            return new ReportsController(
                this.mockReportsService.Object,
                this.mockValidateUserService.Object);
        }

        [Fact]
        public async Task GetAllStatusTaskbyProject_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var reportsController = this.CreateReportsController();
            int lastDays = 0;
            string userId = null;

            // Act
            var result = await reportsController.GetAllStatusTaskbyProject(
                lastDays,
                userId);

            // Assert
            Assert.IsType<UnauthorizedObjectResult>(result);
            this.mockRepository.VerifyAll();
        }
    }
}