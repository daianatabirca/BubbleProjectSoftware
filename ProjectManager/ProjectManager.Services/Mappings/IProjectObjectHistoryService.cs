using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface IProjectObjectHistoryService
    {
        Task<IEnumerable<ProjectObjectHistoryResponse>> GetProjectObjectHistoriesAsync();
    }
}
