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
            CreateMap<Repository.Entities.Project, ProjectRequestPatch>();
            CreateMap<ProjectRequestPatch, Repository.Entities.Project>();
            CreateMap<ProjectResponse, ProjectRequestPatch>();
            CreateMap<ProjectResponse, Repository.Entities.Project>();
        }
    }
}
