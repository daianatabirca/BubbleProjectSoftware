using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace ProjectManager.API.Controllers
{
    [ApiController]
    [Route("api/projectObjectTypes")]
    public class ProjectObjectTypeController : Controller
    {
        private readonly IProjectObjectTypeService _projectObjectTypeService;

        public ProjectObjectTypeController(IProjectObjectTypeService projectObjectTypeService)
        {
            _projectObjectTypeService = projectObjectTypeService ?? throw new ArgumentNullException(nameof(projectObjectTypeService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectObjectTypeResponse>>> GetProjectObjectTypes()
        {
            return Ok(await _projectObjectTypeService.GetProjectObjectTypesAsync());
        }

        [HttpGet("{id}", Name = "GetProjectObjectType")]
        public async Task<ActionResult<ProjectObjectTypeResponse>> GetProjectObjectType(int id)
        {
            var projectObjectType = await _projectObjectTypeService.GetProjectObjectTypeByIdAsync(id);

            if (projectObjectType == null)
            {
                return NotFound(); // if projects doesnt exist
            }

            return Ok(projectObjectType); // 200 ok + returns the project
        }

        [HttpPost]
        public async Task<ActionResult<ProjectObjectTypeResponse>> CreateProjectObjectType([FromBody] ProjectObjectTypeRequest projectObjectTypeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProjectObjectType = await _projectObjectTypeService.CreateProjectObjectTypeAsync(projectObjectTypeRequest); //it was await

            return CreatedAtRoute("GetProjectObjectType", new { id = createdProjectObjectType.Id }, createdProjectObjectType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectObjectTypeResponse>> UpdateProjectObjectType(int id, [FromBody] ProjectObjectTypeRequestUpdate projectObjectTypeRequestUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProjectObjectType = await _projectObjectTypeService.UpdateProjectObjectTypeAsync(projectObjectTypeRequestUpdate, id);

            if (updatedProjectObjectType == null)
            {
                return NotFound();
            }

            return Ok(updatedProjectObjectType);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ProjectObjectTypeResponse>> PatchProject(int id, JsonPatchDocument<ProjectObjectTypeRequestUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            var projectObjectTypeToPatch = await _projectObjectTypeService.AddPatchAsync(id);

            if (projectObjectTypeToPatch == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(projectObjectTypeToPatch, ModelState);

            if (!TryValidateModel(projectObjectTypeToPatch))
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _projectObjectTypeService.UpdateProjectObjectTypeAsync(projectObjectTypeToPatch, id);

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectObjectType(int id)
        {
            var existingProjectObjectType = await _projectObjectTypeService.GetProjectObjectTypeByIdAsync(id);

            if (existingProjectObjectType == null)
            {
                return NotFound();
            }

            await _projectObjectTypeService.DeleteProjectObjectTypeAsync(id);

            return NoContent();
        }
    }
}
