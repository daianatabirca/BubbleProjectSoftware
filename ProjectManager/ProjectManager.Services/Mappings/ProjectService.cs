using AutoMapper;
using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Entities;
using ProjectManager.Repository.Repositories;
using ProjectManager.Services.Exceptions;

namespace ProjectManager.Services.Mappings
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectManagerRepository _projectManagerRepository;
        private readonly IMapper _mapper;

        public ProjectService(IProjectManagerRepository projectManagerRepository, IMapper mapper, IValidator<ProjectRequest> validator)
        {
            _projectManagerRepository = projectManagerRepository ?? throw new ArgumentNullException(nameof(projectManagerRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<IEnumerable<ProjectResponse>> GetProjectsAsync()
        {
            var project = await _projectManagerRepository.GetProjectsAsync();
            return _mapper.Map<IEnumerable<ProjectResponse>>(project);
        }

        public async Task<bool> ProjectExistsAsync(int projectId)
        {
            if (!await _projectManagerRepository.ProjectExistsAsync(projectId))
                return false;
            return true;
        }

        public async Task<ProjectResponse> CreateProjectAsync(ProjectRequest projectRequest)
        {
            var projectEntity = _mapper.Map<Project>(projectRequest);
            _projectManagerRepository.AddProject(projectEntity);

            if (await _projectManagerRepository.SaveChangesAsync())
            {
                return _mapper.Map<ProjectResponse>(projectEntity);
            }

            throw new InternalServerErrorException("The project could not be saved in database.");
        }

        public async Task<ProjectResponse> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectManagerRepository.GetProjectByIdAsync(projectId);

            if (project == null)
            {
                throw new BadRequestException("The project was not found.");
            }

            return (_mapper.Map<Project, ProjectResponse>(project));
        }

        public async Task<ProjectResponse> UpdateProjectAsync(ProjectRequestUpdate projectRequest, int projectId)
        {
            var existingProject = await _projectManagerRepository.GetProjectByIdAsync(projectId);

            if (existingProject == null)
            {
                throw new BadRequestException("The project was not found.");
            }

            var intermediateProject = _mapper.Map(projectRequest, existingProject);

            var updatedProject = await _projectManagerRepository.UpdateAsync(intermediateProject);

            var updatedProjectDto = _mapper.Map<Project, ProjectResponse>(updatedProject);

            return updatedProjectDto;
        }

        public async Task<ProjectRequestPatch> AddPatchAsync(int projectId)
        {
            var existingProject = await GetProjectByIdAsync(projectId);

            if (existingProject == null)
            {
                throw new BadRequestException("The project was not found.");
            }

            var projectToPatch = _mapper.Map<ProjectRequestPatch>(existingProject);
            return projectToPatch;
        }

        public async Task DeleteProjectAsync(int id)
        {
            var existingProject = await _projectManagerRepository.GetProjectByIdAsync(id);

            if (existingProject == null)
            {
                throw new BadRequestException("The project was not found.");
            }

            await _projectManagerRepository.DeleteAsync(existingProject);
        }

        public async Task<ProjectResponse> UpdateProjectPatchAsync(ProjectRequestPatch projectRequest, int projectId)
        {
            var existingProject = await _projectManagerRepository.GetProjectByIdAsync(projectId);

            if (existingProject == null)
            {
                throw new BadRequestException("The project was not found.");
            }

            var intermediateProject = _mapper.Map(projectRequest, existingProject);

            var updatedProject = await _projectManagerRepository.UpdateAsync(intermediateProject);

            var updatedProjectDto = _mapper.Map<Project, ProjectResponse>(updatedProject);

            return updatedProjectDto;
        }
    }
}
