using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace ProjectManager.Controllers
{
    [ApiController]
    [Route("/api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectResponse>>> GetProjects()
        {
            return Ok(await _projectService.GetProjectsAsync());
        }

        [HttpGet("{id}", Name = "GetProject")]
        public async Task<ActionResult<ProjectResponse>> GetProject(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null)
            {
                return NotFound(); // if projects doesnt exist
            }

            return Ok(project); // 200 ok + returns the project
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponse>>CreateProject([FromBody] ProjectRequest projectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProject = await _projectService.CreateProjectAsync(projectRequest); //it was await

            return CreatedAtRoute("GetProject", new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectResponse>> UpdateProject(int id, [FromBody] ProjectRequestUpdate projectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _projectService.UpdateProjectAsync(projectRequest, id);

            if (updatedProject == null)
            {
                return NotFound();
            }

            return Ok(updatedProject);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ProjectResponse>> PatchProject(int id, JsonPatchDocument<ProjectRequestUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            var projectToPatch = await _projectService.AddPatchAsync(id);

            if (projectToPatch == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(projectToPatch, ModelState);

            if (!TryValidateModel(projectToPatch))
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _projectService.UpdateProjectAsync(projectToPatch, id);

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            var existingProject = await _projectService.GetProjectByIdAsync(id);

            if (existingProject == null)
            {
                return NotFound();
            }

            await _projectService.DeleteProjectAsync(id);

            return NoContent();
        }

    }
}
