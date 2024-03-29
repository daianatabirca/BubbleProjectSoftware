using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface IProjectObjectRepository
    {
        //The contract
        Task<IEnumerable<ProjectObject>> GetProjectObjectsAsync();

        Task<bool> ProjectObjectExistsAsync(int projectObjectId);

        //bool ProjectObjectExists(int projectObjectId);

        void AddProjectObject(ProjectObject projectObject);

        Task<bool> SaveChangesAsync();

        Task<ProjectObject?> GetProjectObjectByIdAsync(int projectObjectId);

        Task<ProjectObject> UpdateAsync(ProjectObject projectObject);

        Task DeleteAsync(ProjectObject projectObject);
    }
}
