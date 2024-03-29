using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Profiles
{
    public class ProjectObjectProfile : Profile
    {
        public ProjectObjectProfile()
        {
            CreateMap<ProjectObjectRequest, Repository.Entities.ProjectObject>();
            CreateMap<ProjectObjectResponse, ProjectObjectRequestUpdate>();
            CreateMap<ProjectObjectResponse, ProjectObjectRequestPatch>();
            CreateMap<Repository.Entities.ProjectObject, ProjectObjectResponse>();
            CreateMap<Repository.Entities.ProjectObject, ProjectObjectRequestUpdate>();
            CreateMap<Repository.Entities.ProjectObject, ProjectObjectRequestPatch>();
            CreateMap<ProjectObjectRequestUpdate, Repository.Entities.ProjectObject>();
            CreateMap<ProjectObjectRequestPatch, Repository.Entities.ProjectObject>();
            CreateMap<ProjectObjectResponse, Repository.Entities.ProjectObject>();
        }
    }
}
