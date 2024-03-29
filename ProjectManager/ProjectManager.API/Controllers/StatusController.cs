using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace ProjectManager.API.Controllers
{
    [ApiController]
    [Route("/api/statuses")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService ?? throw new ArgumentNullException(nameof(statusService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusResponse>>> GetStatuses()
        {
            return Ok(await _statusService.GetStatusesAsync());
        }

        [HttpGet("{id}", Name = "GetStatus")]
        public async Task<ActionResult<StatusResponse>> GetStatus(int id)
        {
            var status = await _statusService.GetStatusByIdAsync(id);

            if (status == null)
            {
                return NotFound(); // if projects doesnt exist
            }

            return Ok(status); // 200 ok + returns the project
        }

        [HttpPost]
        public async Task<ActionResult<StatusResponse>> CreateStatus([FromBody] StatusRequest statusRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdStatus = await _statusService.CreateStatusAsync(statusRequest);

            return CreatedAtRoute("GetStatus", new { id = createdStatus.Id }, createdStatus);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StatusResponse>> UpdateStatus(int id, [FromBody] StatusRequestUpdate statusRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedStatus = await _statusService.UpdateStatusAsync(statusRequest, id);

            if (updatedStatus == null)
            {
                return NotFound();
            }

            return Ok(updatedStatus);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<StatusResponse>> PatchStatus(int id, JsonPatchDocument<StatusRequestUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            var statusToPatch = await _statusService.AddPatchAsync(id);

            if (statusToPatch == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(statusToPatch, ModelState);

            if (!TryValidateModel(statusToPatch))
            {
                return BadRequest(ModelState);
            }

            var updatedProject = await _statusService.UpdateStatusAsync(statusToPatch, id);

            return Ok(updatedProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatus(int id)
        {
            var existingStatus = await _statusService.GetStatusByIdAsync(id);

            if (existingStatus == null)
            {
                return NotFound();
            }

            await _statusService.DeleteStatusAsync(id);

            return NoContent();
        }
    }
}
