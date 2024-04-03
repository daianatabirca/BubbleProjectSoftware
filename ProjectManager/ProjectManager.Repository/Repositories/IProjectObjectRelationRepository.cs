using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface IProjectObjectRelationRepository
    {
        //The contract
        Task<IEnumerable<ProjectObjectRelation>> GetProjectObjectRelationsAsync();

        Task<bool> ProjectObjectRelationExistsAsync(int projectObjectRelationId);

        void AddProjectObjectRelation(ProjectObjectRelation projectObjectRelation);

        Task<bool> SaveChangesAsync();

        Task<ProjectObjectRelation?> GetProjectObjectRelationByIdAsync(int projectObjectRelationId);

        Task<ProjectObjectRelation> UpdateAsync(ProjectObjectRelation projectObjectRelation);

        Task DeleteAsync(ProjectObjectRelation projectObjectRelation);
    }
}
