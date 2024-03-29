using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;

namespace CommentsManager.Services.Profiles
{
    public class CommentProfile : Profile
    {
        public CommentProfile() 
        {
            CreateMap<CommentRequest, ProjectManager.Repository.Entities.Comment>();
            CreateMap<CommentResponse, CommentRequestUpdate>();
            CreateMap<CommentResponse, CommentRequestPatch>();
            CreateMap<ProjectManager.Repository.Entities.Comment, CommentResponse>();
            CreateMap<ProjectManager.Repository.Entities.Comment, CommentRequestUpdate>();
            CreateMap<ProjectManager.Repository.Entities.Comment, CommentRequestPatch>();
            CreateMap<CommentRequestUpdate, ProjectManager.Repository.Entities.Comment>();
            CreateMap<CommentRequestPatch, ProjectManager.Repository.Entities.Comment>();
            CreateMap<CommentResponse, ProjectManager.Repository.Entities.Comment>();
        }
    }
}
