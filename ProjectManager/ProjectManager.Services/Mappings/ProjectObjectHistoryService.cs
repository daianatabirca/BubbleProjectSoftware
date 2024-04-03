using AutoMapper;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Repositories;

namespace ProjectManager.Services.Mappings
{
    public class ProjectObjectHistoryService : IProjectObjectHistoryService
    {
        private readonly IProjectObjectHistoryRepository _projectObjectHistoryRepository;
        private readonly IMapper _mapper;

        public ProjectObjectHistoryService(IProjectObjectHistoryRepository projectObjectHistoryRepository, IMapper mapper)
        {
            _projectObjectHistoryRepository = projectObjectHistoryRepository ?? throw new ArgumentNullException(nameof(projectObjectHistoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ProjectObjectHistoryResponse>> GetProjectObjectHistoriesAsync()
        {
            var projectObjectHistory = await _projectObjectHistoryRepository.GetProjectObjectHistoriesAsync();
            return _mapper.Map<IEnumerable<ProjectObjectHistoryResponse>>(projectObjectHistory);
        }
    }
}
