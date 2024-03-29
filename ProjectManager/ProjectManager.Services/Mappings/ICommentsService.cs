using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface ICommentsService
    {
        Task<IEnumerable<CommentsResponse>> GetCommentsAsync();

        Task<bool> CommentsExistsAsync(int CommentsId); //String?

        Task<CommentsResponse> CreateCommentsAsync(CommentsRequest CommentsRequest);

        Task<CommentsResponse?> GetCommentsByIdAsync(int CommentsId);

        Task<CommentsResponse?> UpdateCommentsAsync(CommentsRequestUpdate CommentsRequestUpdate, int CommentsId);

        Task<CommentsResponse?> UpdateCommentsPatchAsync(CommentsRequestPatch CommentsRequestUpdate, int CommentsId);

        Task<CommentsRequestPatch?> AddPatchAsync(int CommentsId);

        Task DeleteCommentsAsync(int CommentsId);
    }
}
