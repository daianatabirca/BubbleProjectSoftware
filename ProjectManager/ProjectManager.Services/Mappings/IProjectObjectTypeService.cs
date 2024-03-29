using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface IProjectObjectTypeService
    {
        Task<IEnumerable<ProjectObjectTypeResponse>> GetProjectObjectTypesAsync();

        Task<bool> ProjectObjectTypeExistsAsync(int projectObjectTypeId);

        Task<ProjectObjectTypeResponse> CreateProjectObjectTypeAsync(ProjectObjectTypeRequest projectObjectTypeRequest);

        Task<ProjectObjectTypeResponse?> GetProjectObjectTypeByIdAsync(int projectObjectTypeId);

        Task<ProjectObjectTypeResponse?> UpdateProjectObjectTypeAsync(ProjectObjectTypeRequestUpdate projectObjectTypeRequestUpdate, int projectObjectTypeId);

        Task<ProjectObjectTypeRequestUpdate?> AddPatchAsync(int projectObjectTypeId);

        Task DeleteProjectObjectTypeAsync(int projectObjectTypeId);
    }
}
