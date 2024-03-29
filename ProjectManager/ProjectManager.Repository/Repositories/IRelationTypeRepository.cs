using ProjectManager.Repository.Entities;

namespace ProjectManager.Repository.Repositories
{
    public interface IRelationTypeRepository
    {
        //The contract
        Task<IEnumerable<RelationType>> GetRelationTypesAsync();

        Task<bool> RelationTypeExistsAsync(int relationTypeId);

        void AddRelationType(RelationType relationType);

        Task<bool> SaveChangesAsync();

        Task<RelationType?> GetRelationTypeByIdAsync(int relationTypeId);

        Task<RelationType> UpdateAsync(RelationType relationType);

        Task DeleteAsync(RelationType relationType);
    }
}
