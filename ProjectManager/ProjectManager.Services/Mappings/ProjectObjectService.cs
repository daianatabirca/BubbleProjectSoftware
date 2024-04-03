using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Entities;
using ProjectManager.Repository.Repositories;
using ProjectManager.Services.DTOs;

namespace ProjectManager.Services.Mappings
{
    public class ProjectObjectService : IProjectObjectService
    {
        private readonly IProjectObjectRepository _projectObjectRepository;
        private readonly IProjectObjectHistoryRepository _projectObjectHistoryRepository;
        private readonly IProjectManagerRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectObjectService(IProjectObjectRepository projectObjectRepository, IProjectObjectHistoryRepository projectObjectHistoryRepository, IProjectManagerRepository projectRepository, IMapper mapper)
        {
            _projectObjectRepository = projectObjectRepository ?? throw new ArgumentNullException(nameof(projectObjectRepository));
            _projectObjectHistoryRepository = projectObjectHistoryRepository ?? throw new ArgumentNullException(nameof(projectObjectHistoryRepository));
            _projectRepository = projectRepository ?? throw new ArgumentNullException(nameof(projectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProjectObjectRequestPatch?> AddPatchAsync(int projectObjectId)
        {
            var existingProjectObject = await GetProjectObjectByIdAsync(projectObjectId);

            if (existingProjectObject == null)
            {
                return null;
            }

            var projectObjectToPatch = _mapper.Map<ProjectObjectRequestPatch>(existingProjectObject);
            return projectObjectToPatch;
        }

        public async Task<ProjectObjectResponse> CreateProjectObjectAsync(ProjectObjectRequest projectObjectRequest)
        {
            var projectId = projectObjectRequest.ProjectId;
            var statusId = projectObjectRequest.StatusId;
            var projectObjectTypeId = projectObjectRequest.ProjectObjectTypeId;

            bool doesProjectExists = await _projectRepository.ProjectExistsAsync(projectId);

            if (!doesProjectExists)
            {
                throw new Exception("ProjectId does not exist!");
            }

            if (!Enum.IsDefined(typeof(DomainModel.Models.Enums.Status), statusId))
            {
                throw new Exception("StatusId does not exist!");
            }

            if (!Enum.IsDefined(typeof(DomainModel.Models.Enums.ProjectObjectType), projectObjectTypeId))
            {
                throw new Exception("ProjectObjectTypeId does not exist!");
            }

            //bool doesStatusExists = await _statusRepository.StatusExistsAsync(statusId);
            //bool doesProjectObjectTypeExists = await _projectObjectTypeRepository.ProjectObjectTypeExistsAsync(projectObjectTypeId);

            //if (!doesStatusExists)
            //{
            //    throw new Exception("StatusId does not exist!");
            //}

            //if (!doesProjectObjectTypeExists)
            //{
            //    throw new Exception("ProjectObjectTypeId does not exist!");
            //}

            var projectObjectEntity = _mapper.Map<ProjectObject>(projectObjectRequest);

            _projectObjectRepository.AddProjectObject(projectObjectEntity);

            if (await _projectObjectRepository.SaveChangesAsync())
            {
                //ADD in ProjectObjectHistory table
                //create the history object
                ProjectObjectHistoryDTO projectObjectHistoryDTO = new ProjectObjectHistoryDTO();

                projectObjectHistoryDTO.CreatedBy = projectObjectRequest.CreatedBy;
                projectObjectHistoryDTO.CreatedDate = projectObjectRequest.CreatedAt;
                projectObjectHistoryDTO.Description = "Project Object '" + projectObjectRequest.Title + "' was created.";
                projectObjectHistoryDTO.ProjectObjectId = projectObjectEntity.Id;

                //map the history object -> to history entity
                var projectObjectHistoryEntity = _mapper.Map<ProjectObjectHistory>(projectObjectHistoryDTO);

                _projectObjectHistoryRepository.AddProjectObjectHistory(projectObjectHistoryEntity);
                await _projectObjectHistoryRepository.SaveChangesAsync();
                //check ProjectObjectHistory

                return _mapper.Map<ProjectObjectResponse>(projectObjectEntity);
            }

            return null; // continue if saving failed
        }

        public async Task DeleteProjectObjectAsync(int projectObjectId)
        {
            var existingProjectObject = await _projectObjectRepository.GetProjectObjectByIdAsync(projectObjectId);

            if (existingProjectObject == null)
            {
                throw new Exception("Project not found");
            }

            //To be redefined for best option
            //Update in ProjectObjectHistory table
            //Create the POHistory object
            ProjectObjectHistoryDTO projectObjectHistoryDTO = new ProjectObjectHistoryDTO();

            projectObjectHistoryDTO.UpdatedBy = existingProjectObject.UpdatedBy;
            projectObjectHistoryDTO.UpdatedDate = existingProjectObject.UpdatedAt;
            projectObjectHistoryDTO.CreatedBy = existingProjectObject.CreatedBy;
            projectObjectHistoryDTO.UpdatedBy = existingProjectObject.UpdatedBy;
            projectObjectHistoryDTO.Description = "Project Object '" + existingProjectObject.Title + "' was deleted.";
            //projectObjectHistoryDTO.ProjectObjectId = intermediateProjectObject.Id;
            projectObjectHistoryDTO.ProjectObjectId = projectObjectId;

            //map the history object -> to history entity
            var projectObjectHistoryEntity = _mapper.Map<ProjectObjectHistory>(projectObjectHistoryDTO);

            _projectObjectHistoryRepository.AddProjectObjectHistory(projectObjectHistoryEntity);
            await _projectObjectHistoryRepository.SaveChangesAsync();
            //check ProjectObjectHistory

            await _projectObjectRepository.DeleteAsync(existingProjectObject);
        }

        public async Task<ProjectObjectResponse?> GetProjectObjectByIdAsync(int projectObjectId)
        {
            var projectObject = await _projectObjectRepository.GetProjectObjectByIdAsync(projectObjectId);
            return (_mapper.Map<ProjectObject, ProjectObjectResponse>(projectObject));
        }

        public async Task<IEnumerable<ProjectObjectResponse>> GetProjectObjectsAsync()
        {
            var projectObject = await _projectObjectRepository.GetProjectObjectsAsync();
            return _mapper.Map<IEnumerable<ProjectObjectResponse>>(projectObject);
        }

        public async Task<bool> ProjectObjectExistsAsync(int projectObjectId)
        {
            if (!await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId))
                return false;
            return true;
        }

        public async Task<ProjectObjectResponse?> UpdateProjectObjectAsync(ProjectObjectRequestUpdate projectObjectRequestUpdate, int projectObjectId)
        {
            var projectId = projectObjectRequestUpdate.ProjectId;
            var statusId = projectObjectRequestUpdate.StatusId;
            var projectObjectTypeId = projectObjectRequestUpdate.ProjectObjectTypeId;

            bool doesProjectExists = await _projectRepository.ProjectExistsAsync(projectId);

            if (!doesProjectExists)
            {
                throw new Exception("ProjectId does not exist!");
            }

            if (!Enum.IsDefined(typeof(DomainModel.Models.Enums.Status), statusId))
            {
                throw new Exception("StatusId does not exist!");
            }

            if (!Enum.IsDefined(typeof(DomainModel.Models.Enums.ProjectObjectType), projectObjectTypeId))
            {
                throw new Exception("ProjectObjectTypeId does not exist!");
            }

            var existingProjectObject = await _projectObjectRepository.GetProjectObjectByIdAsync(projectObjectId);

            if (existingProjectObject == null)
            {
                return null;
            }

            var intermediateProjectObject = _mapper.Map(projectObjectRequestUpdate, existingProjectObject); //updated po entity

            var updatedProjectObject = await _projectObjectRepository.UpdateAsync(intermediateProjectObject);

            //Update in ProjectObjectHistory table
            //Create the POHistory object
            ProjectObjectHistoryDTO projectObjectHistoryDTO = new ProjectObjectHistoryDTO();

            projectObjectHistoryDTO.UpdatedBy = projectObjectRequestUpdate.UpdatedBy;
            projectObjectHistoryDTO.UpdatedDate = projectObjectRequestUpdate.UpdatedAt;
            projectObjectHistoryDTO.CreatedBy = intermediateProjectObject.CreatedBy;
            projectObjectHistoryDTO.CreatedDate = intermediateProjectObject.CreatedAt;
            projectObjectHistoryDTO.Description = "Project Object '" + projectObjectRequestUpdate.Title + "' was updated.";
            //projectObjectHistoryDTO.ProjectObjectId = intermediateProjectObject.Id;
            projectObjectHistoryDTO.ProjectObjectId = projectObjectId;

            //map the history object -> to history entity
            var projectObjectHistoryEntity = _mapper.Map<ProjectObjectHistory>(projectObjectHistoryDTO);

            _projectObjectHistoryRepository.AddProjectObjectHistory(projectObjectHistoryEntity);
            await _projectObjectHistoryRepository.SaveChangesAsync();
            //check ProjectObjectHistory

            var updatedProjectObjectDto = _mapper.Map<ProjectObject, ProjectObjectResponse>(updatedProjectObject);

            return updatedProjectObjectDto;
        }

        public async Task<ProjectObjectResponse?> UpdateProjectObjectPatchAsync(ProjectObjectRequestPatch projectObjectRequestUpdate, int projectObjectId)
        {
            var projectId = projectObjectRequestUpdate.ProjectId;
            var statusId = projectObjectRequestUpdate.StatusId;
            var projectObjectTypeId = projectObjectRequestUpdate.ProjectObjectTypeId;

            bool doesProjectExists = await _projectRepository.ProjectExistsAsync(projectId);

            if (!doesProjectExists)
            {
                throw new Exception("ProjectId does not exist!");
            }

            if (!Enum.IsDefined(typeof(DomainModel.Models.Enums.Status), statusId))
            {
                throw new Exception("StatusId does not exist!");
            }

            if (!Enum.IsDefined(typeof(DomainModel.Models.Enums.ProjectObjectType), projectObjectTypeId))
            {
                throw new Exception("ProjectObjectTypeId does not exist!");
            }

            var existingProjectObject = await _projectObjectRepository.GetProjectObjectByIdAsync(projectObjectId);

            if (existingProjectObject == null)
            {
                return null;
            }

            var intermediateProjectObject = _mapper.Map(projectObjectRequestUpdate, existingProjectObject);

            var updatedProjectObject = await _projectObjectRepository.UpdateAsync(intermediateProjectObject);

            //Update in ProjectObjectHistory table
            //Create the POHistory object
            ProjectObjectHistoryDTO projectObjectHistoryDTO = new ProjectObjectHistoryDTO();

            projectObjectHistoryDTO.UpdatedBy = projectObjectRequestUpdate.UpdatedBy;
            projectObjectHistoryDTO.UpdatedDate = projectObjectRequestUpdate.UpdatedAt;
            projectObjectHistoryDTO.CreatedBy = intermediateProjectObject.CreatedBy;
            projectObjectHistoryDTO.CreatedDate = intermediateProjectObject.CreatedAt;
            projectObjectHistoryDTO.Description = "Project Object '" + projectObjectRequestUpdate.Title + "' was updated.";
            projectObjectHistoryDTO.ProjectObjectId = projectObjectId;

            //map the history object -> to history entity
            var projectObjectHistoryEntity = _mapper.Map<ProjectObjectHistory>(projectObjectHistoryDTO);

            _projectObjectHistoryRepository.AddProjectObjectHistory(projectObjectHistoryEntity);
            await _projectObjectHistoryRepository.SaveChangesAsync();
            //check ProjectObjectHistory

            var updatedProjectObjectDto = _mapper.Map<ProjectObject, ProjectObjectResponse>(updatedProjectObject);

            return updatedProjectObjectDto;
        }
    }
}
