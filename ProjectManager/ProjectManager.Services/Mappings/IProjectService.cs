using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectResponse>> GetProjectsAsync();

        Task<bool> ProjectExistsAsync(int projectId);

        Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequestDto);

        Task<ProjectResponse?> GetProjectByIdAsync(int projectId);

        Task<ProjectResponse?> UpdateProjectAsync(ProjectRequestUpdate projectRequest, int projectId);

        //here to change into ProjectResponse and map with DTO? in method?
        Task<ProjectRequestUpdate?> AddPatchAsync(int projectId);

        Task DeleteProjectAsync(int projectId);
    }
}
