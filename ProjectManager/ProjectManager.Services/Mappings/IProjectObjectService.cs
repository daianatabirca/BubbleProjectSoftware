using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface IProjectObjectService
    {
        Task<IEnumerable<ProjectObjectResponse>> GetProjectObjectsAsync();

        Task<bool> ProjectObjectExistsAsync(int projectObjectId);

        Task<ProjectObjectResponse> CreateProjectObjectAsync(ProjectObjectRequest projectObjectRequest);

        Task<ProjectObjectResponse?> GetProjectObjectByIdAsync(int projectObjectId);

        Task<ProjectObjectResponse?> UpdateProjectObjectAsync(ProjectObjectRequestUpdate projectObjectRequestUpdate, int projectObjectId);
        
        Task<ProjectObjectResponse?> UpdateProjectObjectPatchAsync(ProjectObjectRequestPatch projectObjectRequestUpdate, int projectObjectId);

        Task<ProjectObjectRequestPatch?> AddPatchAsync(int projectObjectId);

        Task DeleteProjectObjectAsync(int projectObjectId);
    }
}
