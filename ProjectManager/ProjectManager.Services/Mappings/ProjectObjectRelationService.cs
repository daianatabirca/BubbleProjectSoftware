using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Entities;
using ProjectManager.Repository.Repositories;

namespace ProjectManager.Services.Mappings
{
    public class ProjectObjectRelationService : IProjectObjectRelationService
    {
        private readonly IProjectObjectRelationRepository _projectObjectRelationRepository;
        private readonly IProjectObjectRepository _projectObjectRepository;
        private readonly IRelationTypeRepository _relationTypeRepository;
        private readonly IMapper _mapper;

        public ProjectObjectRelationService(IProjectObjectRelationRepository projectObjectRelationRepository, IProjectObjectRepository projectObjectRepository, IRelationTypeRepository relationTypeRepository, IMapper mapper)
        {
            _projectObjectRelationRepository = projectObjectRelationRepository ?? throw new ArgumentNullException(nameof(projectObjectRelationRepository));
            _projectObjectRepository = projectObjectRepository ?? throw new ArgumentNullException(nameof(projectObjectRepository));
            _relationTypeRepository = relationTypeRepository ?? throw new ArgumentNullException(nameof(relationTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProjectObjectRelationRequestUpdate?> AddPatchAsync(int projectObjectRelationId)
        {
            var existingProjectObjectRelation = await GetProjectObjectRelationByIdAsync(projectObjectRelationId);

            if (existingProjectObjectRelation == null)
            {
                return null;
            }

            var projectObjectRelationToPatch = _mapper.Map<ProjectObjectRelationRequestUpdate>(existingProjectObjectRelation);
            return projectObjectRelationToPatch;
        }

        public async Task<ProjectObjectRelationResponse> CreateProjectObjectRelationAsync(ProjectObjectRelationRequest projectObjectRelationRequest)
        {
            var projectObjectId = projectObjectRelationRequest.ProjectObjectId;
            var relatedObjectId = projectObjectRelationRequest.RelatedObjectId;
            var relationTypeId = projectObjectRelationRequest.RelationTypeId;

            bool doesProjectObjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId);
            bool doesRelatedProjectObjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(relatedObjectId);
            bool doesRelationTypeIdExists = await _relationTypeRepository.RelationTypeExistsAsync(relationTypeId);

            if (!doesProjectObjectExists)
            {
                throw new Exception("ProjectObjectId does not exist!");
            }

            if (!doesRelatedProjectObjectExists)
            {
                throw new Exception("RelatedObjectId does not exist!");
            }

            if (!doesRelationTypeIdExists)
            {
                throw new Exception("RelationTypeId does not exist!");
            }

            if (projectObjectRelationRequest.ProjectObjectId == projectObjectRelationRequest.RelatedObjectId)
            {
                throw new Exception("ProjectObjectId can not be the same as RelatedObjectId!");
            }

            var projectObjectRelationEntity = _mapper.Map<ProjectObjectRelation>(projectObjectRelationRequest);
            _projectObjectRelationRepository.AddProjectObjectRelation(projectObjectRelationEntity);

            if (await _projectObjectRelationRepository.SaveChangesAsync())
            {
                return _mapper.Map<ProjectObjectRelationResponse>(projectObjectRelationEntity);
            }

            return null; // continue if saving failed
        }

        public async Task DeleteProjectObjectRelationAsync(int projectObjectRelationId)
        {
            var existingProjectObjectRelation = await _projectObjectRelationRepository.GetProjectObjectRelationByIdAsync(projectObjectRelationId);

            if (existingProjectObjectRelation == null)
            {
                throw new Exception("Project not found");
            }

            await _projectObjectRelationRepository.DeleteAsync(existingProjectObjectRelation);
        }

        public async Task<ProjectObjectRelationResponse?> GetProjectObjectRelationByIdAsync(int projectObjectRelationId)
        {
            var projectObjectRelation = await _projectObjectRelationRepository.GetProjectObjectRelationByIdAsync(projectObjectRelationId);
            return (_mapper.Map<ProjectObjectRelation, ProjectObjectRelationResponse>(projectObjectRelation));
        }

        public async Task<IEnumerable<ProjectObjectRelationResponse>> GetProjectObjectRelationsAsync()
        {
            var projectObjectRelation = await _projectObjectRelationRepository.GetProjectObjectRelationsAsync();
            return _mapper.Map<IEnumerable<ProjectObjectRelationResponse>>(projectObjectRelation);
        }

        public async Task<bool> ProjectObjectRelationExistsAsync(int projectObjectRelationId)
        {
            if (!await _projectObjectRelationRepository.ProjectObjectRelationExistsAsync(projectObjectRelationId))
                return false;
            return true;
        }

        public async Task<ProjectObjectRelationResponse?> UpdateProjectObjectRelationAsync(ProjectObjectRelationRequestUpdate projectObjectRelationRequestUpdate, int projectObjectRelationId)
        {
            var projectObjectId = projectObjectRelationRequestUpdate.ProjectObjectId;
            var relatedObjectId = projectObjectRelationRequestUpdate.RelatedObjectId;
            var relationTypeId = projectObjectRelationRequestUpdate.RelationTypeId;

            bool doesProjectObjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId);
            bool doesRelatedProjectObjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(relatedObjectId);
            bool doesRelationTypeIdExists = await _relationTypeRepository.RelationTypeExistsAsync(relationTypeId);

            if (!doesProjectObjectExists)
            {
                throw new Exception("ProjectObjectId does not exist!");
            }

            if (!doesRelatedProjectObjectExists)
            {
                throw new Exception("RelatedObjectId does not exist!");
            }

            if (!doesRelationTypeIdExists)
            {
                throw new Exception("RelationTypeId does not exist!");
            }

            if (projectObjectRelationRequestUpdate.ProjectObjectId == projectObjectRelationRequestUpdate.RelatedObjectId)
            {
                throw new Exception("ProjectObjectId can not be the same as RelatedObjectId!");
            }

            var existingProjectObjectRelation = await _projectObjectRelationRepository.GetProjectObjectRelationByIdAsync(projectObjectRelationId);

            if (existingProjectObjectRelation == null)
            {
                return null;
            }

            var intermediateProjectObjectRelation = _mapper.Map(projectObjectRelationRequestUpdate, existingProjectObjectRelation);

            var updatedProjectObjectRelation = await _projectObjectRelationRepository.UpdateAsync(intermediateProjectObjectRelation);

            var updatedProjectObjectRelationDto = _mapper.Map<ProjectObjectRelation, ProjectObjectRelationResponse>(updatedProjectObjectRelation);

            return updatedProjectObjectRelationDto;
        }
    }
}
