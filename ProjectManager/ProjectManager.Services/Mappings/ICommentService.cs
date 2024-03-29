using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponse>> GetCommentsAsync();

        Task<bool> CommentExistsAsync(int commentsId);

        Task<CommentResponse> CreateCommentAsync(CommentRequest commentsRequest);

        Task<CommentResponse?> GetCommentByIdAsync(int commentsId);

        Task<CommentResponse?> UpdateCommentAsync(CommentRequestUpdate commentsRequestUpdate, int commentsId);

        Task<CommentResponse?> UpdateCommentPatchAsync(CommentRequestPatch commentsRequestUpdate, int commentsId);

        Task<CommentRequestPatch?> AddPatchAsync(int commentsId);
        
        Task DeleteCommentAsync(int commentsId);
    }
}
