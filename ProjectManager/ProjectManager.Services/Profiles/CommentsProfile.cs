using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace CommentsManager.Services.Profiles
{
    public class CommentsProfile : Profile
    {
        public CommentsProfile() 
        {
            CreateMap<CommentsRequest, ProjectManager.Repository.Entities.Comments>();
            CreateMap<CommentsResponse, CommentsRequestUpdate>();
            CreateMap<CommentsResponse, CommentsRequestPatch>();
            CreateMap<ProjectManager.Repository.Entities.Comments, CommentsResponse>();
            CreateMap<ProjectManager.Repository.Entities.Comments, CommentsRequestUpdate>();
            CreateMap<ProjectManager.Repository.Entities.Comments, CommentsRequestPatch>();
            CreateMap<CommentsRequestUpdate, ProjectManager.Repository.Entities.Comments>();
            CreateMap<CommentsRequestPatch, ProjectManager.Repository.Entities.Comments>();
            CreateMap<CommentsResponse, ProjectManager.Repository.Entities.Comments>();
        }
    }
}
