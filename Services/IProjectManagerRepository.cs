using ProjectManager.Entities;

namespace ProjectManager.Services
{
    public interface IProjectManagerRepository
    {
        //The contract
        Task<IEnumerable<Project>> GetProjectsAsync();
    }
}
