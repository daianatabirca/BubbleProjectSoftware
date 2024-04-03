using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace ProjectManager.API.Controllers
{
    [ApiController]
    [Route("api/projectObjectHistories")]
    public class ProjectObjectHistoryController : Controller
    {
        private readonly IProjectObjectHistoryService _projectObjectHistoryService;

        public ProjectObjectHistoryController (IProjectObjectHistoryService projectObjectHistoryService)
        {
            _projectObjectHistoryService = projectObjectHistoryService ?? throw new ArgumentNullException(nameof(projectObjectHistoryService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectObjectHistoryResponse>>> GetProjectObjectHistories()
        {
            return Ok(await _projectObjectHistoryService.GetProjectObjectHistoriesAsync());
        }
    }
}
