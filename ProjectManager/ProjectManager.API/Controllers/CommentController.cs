using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.Mappings;

namespace CommentsManager.API.Controllers
{
    [ApiController]
    [Route("/api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentsService;

        public CommentController(ICommentService commentsService)
        {
            _commentsService = commentsService ?? throw new ArgumentNullException(nameof(commentsService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentResponse>>> GetComments()
        {
            return Ok(await _commentsService.GetCommentsAsync());
        }

        [HttpGet("{id}", Name = "GetComments")]
        public async Task<ActionResult<CommentResponse>> GetComments(int id)
        {
            var comments = await _commentsService.GetCommentByIdAsync(id);

            if (comments == null)
            {
                return NotFound();
            }

            return Ok(comments);
        }

        [HttpPost]
        public async Task<ActionResult<CommentResponse>> CreateComments([FromBody] CommentRequest commentsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdComments = await _commentsService.CreateCommentAsync(commentsRequest);

            return CreatedAtRoute("GetComments", new { id = createdComments.Id }, createdComments);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CommentResponse>> UpdateComments(int id, [FromBody] CommentRequestUpdate commentsRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedComments = await _commentsService.UpdateCommentAsync(commentsRequest, id);

            if (updatedComments == null)
            {
                return NotFound();
            }

            return Ok(updatedComments);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<CommentResponse>> PatchComments(int id, JsonPatchDocument<CommentRequestPatch> patchDocument)
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

            var updatedComments = await _commentsService.UpdateCommentPatchAsync(commentsToPatch, id);

            return Ok(updatedComments);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComments(int id)
        {
            var existingComments = await _commentsService.GetCommentByIdAsync(id);

            if (existingComments == null)
            {
                return NotFound();
            }

            await _commentsService.DeleteCommentAsync(id);

            return NoContent();
        }
    }
}
