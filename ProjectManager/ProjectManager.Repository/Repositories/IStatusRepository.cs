using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface IStatusRepository
    {
        //The contract
        Task<IEnumerable<Status>> GetStatusesAsync();

        Task<bool> StatusExistsAsync(int statusId);

        void AddStatus(Status status);

        Task<bool> SaveChangesAsync();

        Task<Status?> GetStatusByIdAsync(int statusId);

        Task<Status> UpdateAsync(Status status);

        Task DeleteAsync(Status status);
    }
}
