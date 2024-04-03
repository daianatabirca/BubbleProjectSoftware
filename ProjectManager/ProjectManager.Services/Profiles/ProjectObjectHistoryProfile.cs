using AutoMapper;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Services.DTOs;

namespace ProjectManager.Services.Profiles
{
    public class ProjectObjectHistoryProfile : Profile
    {
        public ProjectObjectHistoryProfile() 
        {
            CreateMap<Repository.Entities.ProjectObjectHistory, ProjectObjectHistoryDTO>();
            CreateMap<ProjectObjectHistoryDTO, Repository.Entities.ProjectObjectHistory>();
            CreateMap<ProjectObjectHistoryResponse, Repository.Entities.ProjectObjectHistory>();
            CreateMap<Repository.Entities.ProjectObjectHistory, ProjectObjectHistoryResponse>();
        }
    }
}
