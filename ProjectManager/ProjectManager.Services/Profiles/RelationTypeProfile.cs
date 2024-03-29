using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace ProjectManager.Services.Profiles
{
    public class RelationTypeProfile : Profile
    {
        public RelationTypeProfile() 
        {
            CreateMap<RelationTypeRequest, Repository.Entities.RelationType>();
            CreateMap<RelationTypeResponse, RelationTypeRequestUpdate>();
            CreateMap<Repository.Entities.RelationType, RelationTypeResponse>();
            CreateMap<Repository.Entities.RelationType, RelationTypeRequestUpdate>();
            CreateMap<RelationTypeRequestUpdate, Repository.Entities.RelationType>();
            CreateMap<RelationTypeResponse, Repository.Entities.RelationType>();
        }
    }
}
