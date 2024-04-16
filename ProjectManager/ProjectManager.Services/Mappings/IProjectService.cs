using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponse>> GetProjectsAsync();

        Task<bool> ProjectExistsAsync(int projectId);

        Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest);

        Task<ProjectResponse> GetProjectByIdAsync(int projectId);

        Task<ProjectResponse> UpdateProjectAsync(ProjectRequestUpdate projectRequest, int projectId);

        Task<ProjectResponse> UpdateProjectPatchAsync(ProjectRequestPatch projectRequest, int projectId);

        Task<ProjectRequestPatch> AddPatchAsync(int projectId);

        Task DeleteProjectAsync(int projectId);
    }
}
