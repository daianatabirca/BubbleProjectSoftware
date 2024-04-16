using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.API.Utils;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;
using Serilog;
using System.Net;

namespace ProjectManager.Controllers
{
    [ApiController]
    [Route("/api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        //private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService projectService)
        {
            //_logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            ProjectResponse project;

            try
            {
                project = await _projectService.GetProjectByIdAsync(id);
            }

            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    ErrorCode = (int)HttpStatusCode.BadRequest
                };

                //_logger.LogError("Error accrued at {now}", DateTime.Now);
                Log.Error("Error accrued at {now}", DateTime.Now);
                Log.Error($"Error: {errorResponse.ErrorCode}, {errorResponse.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, errorResponse);
            }

            return Ok(project);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponse>>CreateProject([FromBody] ProjectRequest projectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectResponse createdProject;

            try
            {
                createdProject = await _projectService.CreateProjectAsync(projectRequest);
            }
            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    ErrorCode = (int)HttpStatusCode.InternalServerError
                };

                Log.Error("Error accrued at {now}", DateTime.Now);
                Log.Error($"Error: {errorResponse.ErrorCode}, {errorResponse.Message}");
                return StatusCode((int)HttpStatusCode.InternalServerError, errorResponse);
            }
            
            return CreatedAtRoute("GetProject", new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectResponse>> UpdateProject(int id, [FromBody] ProjectRequestUpdate projectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ProjectResponse updatedProject;

            try
            {
                updatedProject = await _projectService.UpdateProjectAsync(projectRequest, id);
            }

            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    ErrorCode = (int)HttpStatusCode.BadRequest
                };

                Log.Error("Error accrued at {now}", DateTime.Now);
                Log.Error($"Error: {errorResponse.ErrorCode}, {errorResponse.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, errorResponse);
            }

            return Ok(updatedProject);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ProjectResponse>> PatchProject(int id, JsonPatchDocument<ProjectRequestPatch> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            ProjectRequestPatch projectToPatch;

            try
            {
                projectToPatch = await _projectService.AddPatchAsync(id);
            }

            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    ErrorCode = (int)HttpStatusCode.BadRequest
                };

                Log.Error("Error accrued at {now}", DateTime.Now);
                Log.Error($"Error: {errorResponse.ErrorCode}, {errorResponse.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, errorResponse);
            }

            patchDocument.ApplyTo(projectToPatch, ModelState);

            if (!TryValidateModel(projectToPatch))
            {
                return BadRequest(ModelState);
            }

            ProjectResponse updatedProject;

            try
            {
                updatedProject = await _projectService.UpdateProjectPatchAsync(projectToPatch, id);
            }

            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    ErrorCode = (int)HttpStatusCode.BadRequest
                };

                Log.Error("Error accrued at {now}", DateTime.Now);
                Log.Error($"Error: {errorResponse.ErrorCode}, {errorResponse.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, errorResponse);
            }

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProject(int id)
        {
            ProjectResponse existingProject;

            try
            {
                existingProject = await _projectService.GetProjectByIdAsync(id);
            }

            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    ErrorCode = (int)HttpStatusCode.BadRequest
                };

                Log.Error("Error accrued at {now}", DateTime.Now);
                Log.Error($"Error: {errorResponse.ErrorCode}, {errorResponse.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, errorResponse); 
            }

            try
            {
                await _projectService.DeleteProjectAsync(id);
            }

            catch (Exception ex)
            {
                ErrorResponse errorResponse = new ErrorResponse
                {
                    Message = ex.Message,
                    ErrorCode = (int)HttpStatusCode.BadRequest
                };

                Log.Error("Error accrued at {now}", DateTime.Now);
                Log.Error($"Error: {errorResponse.ErrorCode}, {errorResponse.Message}");
                return StatusCode((int)HttpStatusCode.BadRequest, errorResponse);
            }

            return NoContent();
        }

    }
}
