using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectManager.Controllers;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;
using System.Net;
using Xunit;

namespace ProjectManager.API.UnitTests
{
    public class ProjectControllerTests
    {
        [Fact]
        public async Task CreateProject_Test()
        {

            //Numele metodei pe care vreau sa o testez + (When) Cazul testat + (Then) Ceea ce returneaza
            // Arrange
            ProjectRequest projectRequest = new ProjectRequest()
            {
                Name = "Name1",
                Description = "Test Mock",
                StartDate = new DateTime(2016, 7, 2),
                EndDate = new DateTime(2018, 9, 4)
            };

            var mockService = new Mock<IProjectService>(); //mock the ProjectService
            mockService.Setup(repo => repo.CreateProjectAsync(projectRequest))
                .Returns(GetSession); // the method below
            var controller = new ProjectController(mockService.Object);

            // Act
            var result = await controller.CreateProject(projectRequest);

            // Assert
            var actualResult = result.Result as OkObjectResult;

            Assert.Equal((int)HttpStatusCode.Created, actualResult.StatusCode);

            //Assert.True(result.);
        }

        [Fact]
        public async Task GetProjects_Test()
        {
            // Arrange
            var mockService = new Mock<IProjectService>();
            mockService.Setup(repo => repo.GetProjectsAsync())
                .Returns(GetSessions);
            var controller = new ProjectController(mockService.Object);

            // Act
            var result = await controller.GetProjects();

            // Assert
            var viewResult = Assert.IsType<Task<ActionResult<IEnumerable<ProjectResponse>>>>(result);
            var model = Assert.IsAssignableFrom<Task<ActionResult<IEnumerable<ProjectResponse>>>>(viewResult);

            Assert.True(model.IsCompleted);
        }

        [Fact]
        public async Task GetProject_Test()
        {
            // Arrange
            var mockService = new Mock<IProjectService>();
            mockService.Setup(repo => repo.GetProjectByIdAsync(0))
                .Returns(GetSession);
            var controller = new ProjectController(mockService.Object);

            // Act
            var result = await controller.GetProject(0);

            // Assert
            var viewResult = Assert.IsType<Task<ActionResult<ProjectResponse>>>(result);
            var model = Assert.IsAssignableFrom<Task<ActionResult<ProjectResponse>>>(viewResult);

            Assert.True(model.IsCompleted);
        }

        private async Task<ProjectResponse> GetSession()
        {
            var project = new ProjectResponse()
            {
                Name = "Name1",
                Description = "Test One",
                StartDate = new DateTime(2016, 7, 2),
                EndDate = new DateTime(2018, 9, 4)
            };

            return project;
        }

        private async Task<IEnumerable<ProjectResponse>> GetSessions()
        {
            var projects = new List<ProjectResponse>
            {
                new ProjectResponse()
                {
                    Name = "Name1",
                    Description = "Test One",
                    StartDate = new DateTime(2016, 7, 2),
                    EndDate = new DateTime(2018, 9, 4)
                },
                new ProjectResponse()
                {
                    Name = "Name1",
                    Description = "Test Two",
                    StartDate = new DateTime(2016, 7, 2),
                    EndDate = new DateTime(2018, 9, 4)
                }
            };

            return projects;
        }

    }
}