using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile() 
        {
            CreateMap<ProjectRequest, Repository.Entities.Project>();
            CreateMap<ProjectResponse, ProjectRequestUpdate>();
            CreateMap<Repository.Entities.Project, ProjectResponse>();
            CreateMap<Repository.Entities.Project, ProjectRequestUpdate>();
            CreateMap<ProjectRequestUpdate, Repository.Entities.Project>();
            CreateMap<ProjectResponse, Repository.Entities.Project>();
        }
    }
}
