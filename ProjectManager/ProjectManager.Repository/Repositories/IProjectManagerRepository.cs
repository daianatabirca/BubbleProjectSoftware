using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface IProjectManagerRepository
    {
        //The contract
        Task<IEnumerable<Project>> GetProjectsAsync();

        Task<bool> ProjectExistsAsync(int projectId);

        void AddProject(Project project);

        Task<bool> SaveChangesAsync();

        Task<Project?> GetProjectByIdAsync(int projectId);

        Task<Project> UpdateAsync(Project project);

        Task DeleteAsync(Project project);
    }
}
