using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface IRelationTypeService
    {
        Task<IEnumerable<RelationTypeResponse>> GetRelationTypesAsync();

        Task<bool> RelationTypeExistsAsync(int relationTypeId);

        Task<RelationTypeResponse> CreateRelationTypeAsync(RelationTypeRequest relationTypeRequest);

        Task<RelationTypeResponse?> GetRelationTypeByIdAsync(int relationTypeId);

        Task<RelationTypeResponse?> UpdateRelationTypeAsync(RelationTypeRequestUpdate relationTypeRequestUpdate, int relationTypeId);

        Task<RelationTypeRequestUpdate?> AddPatchAsync(int relationTypeId);

        Task DeleteRelationTypeAsync(int relationTypeId);
    }
}
