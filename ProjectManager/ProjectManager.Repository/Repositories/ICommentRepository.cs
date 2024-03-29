using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface ICommentRepository
    {
        //The contract
        Task<IEnumerable<Comment>> GetCommentsAsync();

        Task<bool> CommentExistsAsync(int CommentsId);

        void AddComment(Comment Comments);

        Task<bool> SaveChangesAsync();

        Task<Comment?> GetCommentByIdAsync(int CommentsId);

        Task<Comment> UpdateAsync(Comment Comments);

        Task DeleteAsync(Comment Comments);
    }
}
