using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace ProjectManager.API.Controllers
{
    [ApiController]
    [Route("api/projectObjectRelations")]
    public class ProjectObjectRelationController : Controller
    {
        private readonly IProjectObjectRelationService _projectObjectRelationService;

        public ProjectObjectRelationController(IProjectObjectRelationService projectObjectRelationService)
        {
            _projectObjectRelationService = projectObjectRelationService ?? throw new ArgumentNullException(nameof(projectObjectRelationService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectObjectRelationResponse>>> GetProjectObjectRelations()
        {
            return Ok(await _projectObjectRelationService.GetProjectObjectRelationsAsync());
        }

        [HttpGet("{id}", Name = "GetProjectObjectRelation")]
        public async Task<ActionResult<ProjectObjectRelationResponse>> GetProjectObjectRelation(int id)
        {
            var projectObjectRelation = await _projectObjectRelationService.GetProjectObjectRelationByIdAsync(id);

            if (projectObjectRelation == null)
            {
                return NotFound(); // if projects doesnt exist
            }

            return Ok(projectObjectRelation); // 200 ok + returns the project
        }

        [HttpPost]
        public async Task<ActionResult<ProjectObjectRelationResponse>> CreateProjectObjectRelation([FromBody] ProjectObjectRelationRequest projectObjectRelationRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProjectObjectRelation = await _projectObjectRelationService.CreateProjectObjectRelationAsync(projectObjectRelationRequest); //it was await

            return CreatedAtRoute("GetProjectObjectRelation", new { id = createdProjectObjectRelation.Id }, createdProjectObjectRelation);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectObjectRelationResponse>> UpdateProjectObjectRelation(int id, [FromBody] ProjectObjectRelationRequestUpdate projectObjectRelationRequestUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedProjectObjectRelation = await _projectObjectRelationService.UpdateProjectObjectRelationAsync(projectObjectRelationRequestUpdate, id);

            if (updatedProjectObjectRelation == null)
            {
                return NotFound();
            }

            return Ok(updatedProjectObjectRelation);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<ProjectObjectRelationResponse>> PatchProject(int id, JsonPatchDocument<ProjectObjectRelationRequestUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            var projectObjectRelationToPatch = await _projectObjectRelationService.AddPatchAsync(id);

            if (projectObjectRelationToPatch == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(projectObjectRelationToPatch, ModelState);

            if (!TryValidateModel(projectObjectRelationToPatch))
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _projectObjectRelationService.UpdateProjectObjectRelationAsync(projectObjectRelationToPatch, id);

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectObjectRelation(int id)
        {
            var existingProjectObjectRelation = await _projectObjectRelationService.GetProjectObjectRelationByIdAsync(id);

            if (existingProjectObjectRelation == null)
            {
                return NotFound();
            }

            await _projectObjectRelationService.DeleteProjectObjectRelationAsync(id);

            return NoContent();
        }
    }
}
