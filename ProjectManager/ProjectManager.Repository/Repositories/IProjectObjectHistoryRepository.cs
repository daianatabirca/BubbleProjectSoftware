using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface IProjectObjectHistoryRepository
    {
        //The contract
        Task<IEnumerable<ProjectObjectHistory>> GetProjectObjectHistoriesAsync();

        Task<bool> ProjectObjectHistoryExistsAsync(int projectObjectHistoryId);

        void AddProjectObjectHistory(ProjectObjectHistory projectObjectHistory);

        Task<bool> SaveChangesAsync();

        Task<ProjectObjectHistory?> GetProjectObjectHistoryByIdAsync(int projectObjectHistoryId);

        Task<ProjectObjectHistory> UpdateAsync(ProjectObjectHistory projectObjectHistory);

        Task DeleteAsync(ProjectObjectHistory projectObjectHistory);
    }
}
