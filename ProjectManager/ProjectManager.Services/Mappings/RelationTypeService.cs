using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Repositories;
using RelationType = ProjectManager.Repository.Entities.RelationType;

namespace ProjectManager.Services.Mappings
{
    public class RelationTypeService : IRelationTypeService
    {
        private readonly IRelationTypeRepository _relationTypeRepository;
        private readonly IMapper _mapper;

        public RelationTypeService(IRelationTypeRepository relationTypeRepository, IMapper mapper)
        {
            _relationTypeRepository = relationTypeRepository ?? throw new ArgumentNullException(nameof(relationTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<RelationTypeRequestUpdate?> AddPatchAsync(int relationTypeId)
        {
            var existingRelationType = await GetRelationTypeByIdAsync(relationTypeId);

            if (existingRelationType == null)
            {
                return null;
            }

            var relationTypeToPatch = _mapper.Map<RelationTypeRequestUpdate>(existingRelationType);
            return relationTypeToPatch;
        }

        public async Task<RelationTypeResponse> CreateRelationTypeAsync(RelationTypeRequest relationTypeRequest)
        {
            var relationTypeEntity = _mapper.Map<RelationType>(relationTypeRequest);
            _relationTypeRepository.AddRelationType(relationTypeEntity);

            if (await _relationTypeRepository.SaveChangesAsync())
            {
                return _mapper.Map<RelationTypeResponse>(relationTypeEntity);
            }

            return null; // continue if saving failed
        }

        public async Task DeleteRelationTypeAsync(int relationTypeId)
        {
            var existingRelationType = await _relationTypeRepository.GetRelationTypeByIdAsync(relationTypeId);

            if (existingRelationType == null)
            {
                throw new Exception("Project not found");
            }

            await _relationTypeRepository.DeleteAsync(existingRelationType);
        }

        public async Task<RelationTypeResponse?> GetRelationTypeByIdAsync(int relationTypeId)
        {
            var relationType = await _relationTypeRepository.GetRelationTypeByIdAsync(relationTypeId);
            return (_mapper.Map<RelationType, RelationTypeResponse>(relationType));
        }

        public async Task<IEnumerable<RelationTypeResponse>> GetRelationTypesAsync()
        {
            var relationType = await _relationTypeRepository.GetRelationTypesAsync();
            return _mapper.Map<IEnumerable<RelationTypeResponse>>(relationType);
        }

        public async Task<bool> RelationTypeExistsAsync(int relationTypeId)
        {
            if (!await _relationTypeRepository.RelationTypeExistsAsync(relationTypeId))
                return false;
            return true;
        }

        public async Task<RelationTypeResponse?> UpdateRelationTypeAsync(RelationTypeRequestUpdate relationTypeRequestUpdate, int relationTypeId)
        {
            var existingRelationType = await _relationTypeRepository.GetRelationTypeByIdAsync(relationTypeId);

            if (existingRelationType == null)
            {
                return null;
            }

            var intermediateRelationType = _mapper.Map(relationTypeRequestUpdate, existingRelationType);

            var updatedRelationType = await _relationTypeRepository.UpdateAsync(intermediateRelationType);

            var updatedRelationTypeDto = _mapper.Map<RelationType, RelationTypeResponse>(updatedRelationType);

            return updatedRelationTypeDto;
        }
    }
}
