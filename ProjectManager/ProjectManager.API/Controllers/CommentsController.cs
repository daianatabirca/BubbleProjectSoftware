using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace CommentsManager.API.Controllers
{
    [ApiController]
    [Route("/api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;

        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService ?? throw new ArgumentNullException(nameof(commentsService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentsResponse>>> GetComments()
        {
            return Ok(await _commentsService.GetCommentsAsync());
        }

        [HttpGet("{id}", Name = "GetComments")]
        public async Task<ActionResult<CommentsResponse>> GetComments(int id)
        {
            var comments = await _commentsService.GetCommentsByIdAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<CommentsResponse>> CreateComments([FromBody] CommentsRequest commentsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdComments = await _commentsService.CreateCommentsAsync(commentsRequest);

            return CreatedAtRoute("GetComments", new { id = createdComments.Id }, createdComments);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentsResponse>> UpdateComments(int id, [FromBody] CommentsRequestUpdate commentsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedComments = await _commentsService.UpdateCommentsAsync(commentsRequest, id);

            if (updatedComments == null)
            {
                return NotFound();
            }

            return Ok(updatedComments);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CommentsResponse>> PatchComments(int id, JsonPatchDocument<CommentsRequestPatch> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            var commentsToPatch = await _commentsService.AddPatchAsync(id);

            if (commentsToPatch == null)
            {
                return NotFound();
            }

            patchDocument.ApplyTo(commentsToPatch, ModelState);

            if (!TryValidateModel(commentsToPatch))
            {
                return BadRequest(ModelState);
            }

            var updatedComments = await _commentsService.UpdateCommentsPatchAsync(commentsToPatch, id);

            return Ok(updatedComments);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComments(int id)
        {
            var existingComments = await _commentsService.GetCommentsByIdAsync(id);

            if (existingComments == null)
            {
                return NotFound();
            }

            await _commentsService.DeleteCommentsAsync(id);

            return NoContent();
        }
    }
}
