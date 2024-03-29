using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface IProjectObjectTypeRepository
    {
        //The contract
        Task<IEnumerable<ProjectObjectType>> GetProjectObjectTypesAsync();

        Task<bool> ProjectObjectTypeExistsAsync(int projectObjectTypeId);

        void AddProjectObjectType(ProjectObjectType projectObjectType);

        Task<bool> SaveChangesAsync();

        Task<ProjectObjectType?> GetProjectObjectTypeByIdAsync(int projectObjectTypeId);

        Task<ProjectObjectType> UpdateAsync(ProjectObjectType projectObjectType);

        Task DeleteAsync(ProjectObjectType projectObjectType);
    }
}
