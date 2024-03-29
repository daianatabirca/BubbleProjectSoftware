using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Profiles
{
    public class ProjectObjectTypeProfile : Profile
    {
        public ProjectObjectTypeProfile() 
        {
            CreateMap<ProjectObjectTypeRequest, Repository.Entities.ProjectObjectType>();
            CreateMap<ProjectObjectTypeResponse, ProjectObjectTypeRequestUpdate>();
            CreateMap<Repository.Entities.ProjectObjectType, ProjectObjectTypeResponse>();
            CreateMap<Repository.Entities.ProjectObjectType, ProjectObjectTypeRequestUpdate>();
            CreateMap<ProjectObjectTypeRequestUpdate, Repository.Entities.ProjectObjectType>();
            CreateMap<ProjectObjectTypeResponse, Repository.Entities.ProjectObjectType>();
        }
    }
}
