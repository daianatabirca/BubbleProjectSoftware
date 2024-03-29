using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface ICommentsRepository
    {
        //The contract
        Task<IEnumerable<Comments>> GetCommentsAsync();

        Task<bool> CommentsExistsAsync(int CommentsId);

        void AddComments(Comments Comments);

        Task<bool> SaveChangesAsync();

        Task<Comments?> GetCommentsByIdAsync(int CommentsId);

        Task<Comments> UpdateAsync(Comments Comments);

        Task DeleteAsync(Comments Comments);
    }
}
