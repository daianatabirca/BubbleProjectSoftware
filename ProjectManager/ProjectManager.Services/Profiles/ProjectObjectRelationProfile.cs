using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Profiles
{
    public class ProjectObjectRelationProfile : Profile
    {
        public ProjectObjectRelationProfile()
        {
            CreateMap<ProjectObjectRelationRequest, Repository.Entities.ProjectObjectRelation>();
            CreateMap<ProjectObjectRelationResponse, ProjectObjectRelationRequestUpdate>();
            CreateMap<Repository.Entities.ProjectObjectRelation, ProjectObjectRelationResponse>();
            CreateMap<Repository.Entities.ProjectObjectRelation, ProjectObjectRelationRequestUpdate>();
            CreateMap<ProjectObjectRelationRequestUpdate, Repository.Entities.ProjectObjectRelation>();
            CreateMap<ProjectObjectRelationResponse, Repository.Entities.ProjectObjectRelation>();
        }
    }
}
