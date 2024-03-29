using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Mappings
{
    public interface IStatusService
    {
        Task<IEnumerable<StatusResponse>> GetStatusesAsync();

        Task<bool> StatusExistsAsync(int statusId); //String?

        Task<StatusResponse> CreateStatusAsync(StatusRequest statusRequest);

        Task<StatusResponse?> GetStatusByIdAsync(int statusId);

        Task<StatusResponse?> UpdateStatusAsync(StatusRequestUpdate statusRequestUpdate, int statusId);

        //here to change?
        Task<StatusRequestUpdate?> AddPatchAsync(int statusId);

        Task DeleteStatusAsync(int statusId);
    }
}
