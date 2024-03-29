using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace ProjectManager.API.Controllers
{
    [ApiController]
    [Route("/api/relationTypes")]
    public class RelationTypeController : ControllerBase
    {
        private readonly IRelationTypeService _relationTypeService;

        public RelationTypeController(IRelationTypeService relationTypeService)
        {
            _relationTypeService = relationTypeService ?? throw new ArgumentNullException(nameof(relationTypeService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationTypeResponse>>> GetRelationTypes()
        {
            return Ok(await _relationTypeService.GetRelationTypesAsync());
        }

        [HttpGet("{id}", Name = "GetRelationType")]
        public async Task<ActionResult<RelationTypeResponse>> GetRelationType(int id)
        {
            var relationType = await _relationTypeService.GetRelationTypeByIdAsync(id);

            if (relationType == null)
            {
                return NotFound(); // if projects doesnt exist
            }

            return Ok(relationType); // 200 ok + returns the project
        }

        [HttpPost]
        public async Task<ActionResult<RelationTypeResponse>> CreateRelationType([FromBody] RelationTypeRequest relationTypeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRelationType = await _relationTypeService.CreateRelationTypeAsync(relationTypeRequest);

            return CreatedAtRoute("GetRelationType", new { id = createdRelationType.Id }, createdRelationType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<RelationTypeResponse>> UpdateRelationType(int id, [FromBody] RelationTypeRequestUpdate relationTypeRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedRelationType = await _relationTypeService.UpdateRelationTypeAsync(relationTypeRequest, id);

            if (updatedRelationType == null)
            {
                return NotFound();
            }

            return Ok(updatedRelationType);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<RelationTypeResponse>> PatchStatus(int id, JsonPatchDocument<RelationTypeRequestUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            var relationTypeToPatch = await _relationTypeService.AddPatchAsync(id);

            if (relationTypeToPatch == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(relationTypeToPatch, ModelState);

            if (!TryValidateModel(relationTypeToPatch))
            {
                return BadRequest(ModelState);
            }

            var updatedRelationType = await _relationTypeService.UpdateRelationTypeAsync(relationTypeToPatch, id);

            return Ok(updatedRelationType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
            var existingRelationType = await _relationTypeService.GetRelationTypeByIdAsync(id);

            if (existingRelationType == null)
            {
                return NotFound();
            }

            await _relationTypeService.DeleteRelationTypeAsync(id);

            return NoContent();
        }
    }
}
