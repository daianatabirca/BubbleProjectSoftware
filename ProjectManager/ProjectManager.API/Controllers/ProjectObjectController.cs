using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace ProjectManager.API.Controllers
{
    [ApiController]
    [Route("/api/projectObjects")]
    public class ProjectObjectController : ControllerBase
    {
        private readonly IProjectObjectService _projectObjectService;

        public ProjectObjectController(IProjectObjectService projectObjectService)
        {
            _projectObjectService = projectObjectService ?? throw new ArgumentNullException(nameof(projectObjectService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectObjectResponse>>> GetProjectObjects()
        {
            return Ok(await _projectObjectService.GetProjectObjectsAsync());
        }

        [HttpGet("{id}", Name = "GetProjectObject")]
        public async Task<ActionResult<ProjectObjectResponse>> GetProjectObject(int id)
        {
            var projectObject = await _projectObjectService.GetProjectObjectByIdAsync(id);

            if (projectObject == null)
            {
                return NotFound(); // if projects doesnt exist
            }

            return Ok(projectObject); // 200 ok + returns the project
        }

        [HttpPost]
        public async Task<ActionResult<ProjectObjectResponse>> CreateProjectObject([FromBody] ProjectObjectRequest projectObjectRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProjectObject = await _projectObjectService.CreateProjectObjectAsync(projectObjectRequest); //it was await

            return CreatedAtRoute("GetProjectObject", new { id = createdProjectObject.Id }, createdProjectObject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectObjectResponse>> UpdateProjectObject(int id, [FromBody] ProjectObjectRequestUpdate projectObjectRequestUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProjectObject = await _projectObjectService.UpdateProjectObjectAsync(projectObjectRequestUpdate, id);

            if (updatedProjectObject == null)
            {
                return NotFound();
            }

            return Ok(updatedProjectObject);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ProjectObjectResponse>> PatchProject(int id, JsonPatchDocument<ProjectObjectRequestPatch> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            var projectObjectToPatch = await _projectObjectService.AddPatchAsync(id);

            if (projectObjectToPatch == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(projectObjectToPatch, ModelState);

            if (!TryValidateModel(projectObjectToPatch))
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _projectObjectService.UpdateProjectObjectPatchAsync(projectObjectToPatch, id);

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectObject(int id)
        {
            var existingProjectObject = await _projectObjectService.GetProjectObjectByIdAsync(id);

            if (existingProjectObject == null)
            {
                return NotFound();
            }

            await _projectObjectService.DeleteProjectObjectAsync(id);

            return NoContent();
        }
    }
}
