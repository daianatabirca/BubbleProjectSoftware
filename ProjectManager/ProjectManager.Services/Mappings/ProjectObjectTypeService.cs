using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Entities;
using ProjectManager.Repository.Repositories;

namespace ProjectManager.Services.Mappings
{
    public class ProjectObjectTypeService : IProjectObjectTypeService
    {
        private readonly IProjectObjectTypeRepository _projectObjectTypeRepository;
        private readonly IMapper _mapper;

        public ProjectObjectTypeService (IProjectObjectTypeRepository projectObjectTypeRepository, IMapper mapper)
        {
            _projectObjectTypeRepository = projectObjectTypeRepository ?? throw new ArgumentNullException(nameof(projectObjectTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProjectObjectTypeRequestUpdate?> AddPatchAsync(int projectObjectTypeId)
        {
            var existingProjectObjectType = await GetProjectObjectTypeByIdAsync(projectObjectTypeId);

            if (existingProjectObjectType == null)
            {
                return null;
            }

            var projectObjectTypeToPatch = _mapper.Map<ProjectObjectTypeRequestUpdate>(existingProjectObjectType);
            return projectObjectTypeToPatch;
        }

        public async Task<ProjectObjectTypeResponse> CreateProjectObjectTypeAsync(ProjectObjectTypeRequest projectObjectTypeRequest)
        {
            var projectObjectTypeEntity = _mapper.Map<ProjectObjectType>(projectObjectTypeRequest);
            _projectObjectTypeRepository.AddProjectObjectType(projectObjectTypeEntity);

            if (await _projectObjectTypeRepository.SaveChangesAsync())
            {
                return _mapper.Map<ProjectObjectTypeResponse>(projectObjectTypeEntity);
            }

            return null; // continue if saving failed
        }

        public async Task DeleteProjectObjectTypeAsync(int projectObjectTypeId)
        {
            var existingProjectObjectType = await _projectObjectTypeRepository.GetProjectObjectTypeByIdAsync(projectObjectTypeId);

            if (existingProjectObjectType == null)
            {
                throw new Exception("Project not found");
            }

            await _projectObjectTypeRepository.DeleteAsync(existingProjectObjectType);
        }

        public async Task<ProjectObjectTypeResponse?> GetProjectObjectTypeByIdAsync(int projectObjectTypeId)
        {
            var projectObjectType = await _projectObjectTypeRepository.GetProjectObjectTypeByIdAsync(projectObjectTypeId);
            return (_mapper.Map<ProjectObjectType, ProjectObjectTypeResponse>(projectObjectType));
        }

        public async Task<IEnumerable<ProjectObjectTypeResponse>> GetProjectObjectTypesAsync()
        {
            var projectObjectType = await _projectObjectTypeRepository.GetProjectObjectTypesAsync();
            return _mapper.Map<IEnumerable<ProjectObjectTypeResponse>>(projectObjectType);
        }

        public async Task<bool> ProjectObjectTypeExistsAsync(int projectObjectTypeId)
        {
            if (!await _projectObjectTypeRepository.ProjectObjectTypeExistsAsync(projectObjectTypeId))
                return false;
            return true;
        }

        public async Task<ProjectObjectTypeResponse?> UpdateProjectObjectTypeAsync(ProjectObjectTypeRequestUpdate projectObjectTypeRequestUpdate, int projectObjectTypeId)
        {
            var existingProjectObjectType = await _projectObjectTypeRepository.GetProjectObjectTypeByIdAsync(projectObjectTypeId);

            if (existingProjectObjectType == null)
            {
                return null;
            }

            var intermediateProjectObjectType = _mapper.Map(projectObjectTypeRequestUpdate, existingProjectObjectType);

            var updatedProjectObjectType = await _projectObjectTypeRepository.UpdateAsync(intermediateProjectObjectType);

            var updatedProjectObjectTypeDto = _mapper.Map<ProjectObjectType, ProjectObjectTypeResponse>(updatedProjectObjectType);

            return updatedProjectObjectTypeDto;
        }
    }
}
