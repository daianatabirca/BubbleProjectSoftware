using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface IProjectObjectRelationService
    {
        Task<IEnumerable<ProjectObjectRelationResponse>> GetProjectObjectRelationsAsync();

        Task<bool> ProjectObjectRelationExistsAsync(int projectObjectRelationId);

        Task<ProjectObjectRelationResponse> CreateProjectObjectRelationAsync(ProjectObjectRelationRequest projectObjectRelationRequest);

        Task<ProjectObjectRelationResponse?> GetProjectObjectRelationByIdAsync(int projectObjectRelationId);

        Task<ProjectObjectRelationResponse?> UpdateProjectObjectRelationAsync(ProjectObjectRelationRequestUpdate projectObjectRelationRequestUpdate, int projectObjectRelationId);

        Task<ProjectObjectRelationRequestUpdate?> AddPatchAsync(int projectObjectRelationId);

        Task DeleteProjectObjectRelationAsync(int projectObjectRelationId);
    }
}
