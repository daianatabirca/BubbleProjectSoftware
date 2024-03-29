using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Entities;
using ProjectManager.Repository.Repositories;
using System;

namespace ProjectManager.Services.Mappings
{
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        private readonly IMapper _mapper;

        public StatusService(IStatusRepository statusRepository, IMapper mapper)
        {
            _statusRepository = statusRepository ?? throw new ArgumentNullException(nameof(statusRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<StatusRequestUpdate?> AddPatchAsync(int statusId)
        {
            var existingStatus = await GetStatusByIdAsync(statusId);

            if (existingStatus == null)
            {
                return null;
            }

            var statusToPatch = _mapper.Map<StatusRequestUpdate>(existingStatus);
            return statusToPatch;
        }

        public async Task<StatusResponse> CreateStatusAsync(StatusRequest statusRequest)
        {
            var statusEntity = _mapper.Map<Status>(statusRequest);
            _statusRepository.AddStatus(statusEntity);

            if (await _statusRepository.SaveChangesAsync())
            {
                return _mapper.Map<StatusResponse>(statusEntity);
            }

            return null; // continue if saving failed
        }

        public async Task DeleteStatusAsync(int statusId)
        {
            var existingStatus = await _statusRepository.GetStatusByIdAsync(statusId);

            if (existingStatus == null)
            {
                throw new Exception("Project not found");
            }

            await _statusRepository.DeleteAsync(existingStatus);
        }

        public async Task<StatusResponse?> GetStatusByIdAsync(int statusId)
        {
            var status = await _statusRepository.GetStatusByIdAsync(statusId);
            return (_mapper.Map<Status, StatusResponse>(status));
        }

        public async Task<IEnumerable<StatusResponse>> GetStatusesAsync()
        {
            var status = await _statusRepository.GetStatusesAsync();
            return _mapper.Map<IEnumerable<StatusResponse>>(status);
        }

        public async Task<bool> StatusExistsAsync(int statusId)
        {
            if (!await _statusRepository.StatusExistsAsync(statusId))
                return false;
            return true;
        }

        public async Task<StatusResponse?> UpdateStatusAsync(StatusRequestUpdate statusRequestUpdate, int statusId)
        {
            var existingStatus = await _statusRepository.GetStatusByIdAsync(statusId);

            if (existingStatus == null)
            {
                return null;
            }

            var intermediateStatus = _mapper.Map(statusRequestUpdate, existingStatus);

            var updatedStatus = await _statusRepository.UpdateAsync(intermediateStatus);

            var updatedStatusDto = _mapper.Map<Status, StatusResponse>(updatedStatus);

            return updatedStatusDto;
        }
    }
}
