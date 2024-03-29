using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Entities;
using ProjectManager.Repository.Repositories;

namespace ProjectManager.Services.Mappings
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentsRepository;
        private readonly IProjectObjectRepository _projectObjectRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentsRepository, IMapper mapper, IProjectObjectRepository projectObjectRepository)
        {
            _commentsRepository = commentsRepository ?? throw new ArgumentNullException(nameof(commentsRepository));
            _projectObjectRepository = projectObjectRepository ?? throw new ArgumentNullException(nameof(projectObjectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _projectObjectRepository = projectObjectRepository;
        }

        public async Task<CommentRequestPatch?> AddPatchAsync(int commentsId)
        {
            var existingComment = await GetCommentByIdAsync(commentsId);

            if (existingComment == null)
            {
                return null;
            }

            var commentToPatch = _mapper.Map<CommentRequestPatch>(existingComment);
            return commentToPatch;
        }

        public async Task<bool> CommentExistsAsync(int commentsId)
        {
            if (!await _commentsRepository.CommentExistsAsync(commentsId))
                return false;
            return true;
        }

        public async Task<CommentResponse> CreateCommentAsync(CommentRequest commentsRequest)
        {
            var projectObjectId = commentsRequest.ProjectObjectId;

            bool doesProjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId);

            if (!doesProjectExists) //if project does not exists thorw error
            {
                throw new Exception("ProjectId does not exist!");
            }

            var commentsEntity = _mapper.Map<Comment>(commentsRequest);
            _commentsRepository.AddComment(commentsEntity);

            if (await _commentsRepository.SaveChangesAsync())
            {
                return _mapper.Map<CommentResponse>(commentsEntity);
            }

            return null; // continue if saving failed
        }

        public async Task DeleteCommentAsync(int commentsId)
        {
            var existingComments = await _commentsRepository.GetCommentByIdAsync(commentsId);

            if (existingComments == null)
            {
                throw new Exception("Project not found");
            }

            await _commentsRepository.DeleteAsync(existingComments);
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsAsync()
        {
            var comments = await _commentsRepository.GetCommentsAsync();
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }

        public async Task<CommentResponse?> GetCommentByIdAsync(int commentsId)
        {
            var comments = await _commentsRepository.GetCommentByIdAsync(commentsId);
            return _mapper.Map<Comment, CommentResponse>(comments);
        }

        public async Task<CommentResponse?> UpdateCommentAsync(CommentRequestUpdate commentsRequestUpdate, int commentsId)
        {
            var projectObjectId = commentsRequestUpdate.ProjectObjectId;

            bool doesProjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId);

            if (!doesProjectExists) //if project does not exists thorw error
            {
                throw new Exception("ProjectId does not exist!");
            }

            var existingComments = await _commentsRepository.GetCommentByIdAsync(commentsId);

            if (existingComments == null)
            {
                return null;
            }

            var intermediateComments = _mapper.Map(commentsRequestUpdate, existingComments);

            var updatedComments = await _commentsRepository.UpdateAsync(intermediateComments);

            var updatedCommentsDto = _mapper.Map<Comment, CommentResponse>(updatedComments);

            return updatedCommentsDto;
        }

        public async Task<CommentResponse?> UpdateCommentPatchAsync(CommentRequestPatch commentsRequestUpdate, int commentsId)
        {
            var projectObjectId = commentsRequestUpdate.ProjectObjectId;

            bool doesProjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId);

            if (!doesProjectExists) //if project does not exists thorw error
            {
                throw new Exception("ProjectId does not exist!");
            }

            var existingComments = await _commentsRepository.GetCommentByIdAsync(commentsId);

            if (existingComments == null)
            {
                return null;
            }

            var intermediateComments = _mapper.Map(commentsRequestUpdate, existingComments);

            var updatedComments = await _commentsRepository.UpdateAsync(intermediateComments);

            var updatedCommentsDto = _mapper.Map<Comment, CommentResponse>(updatedComments);

            return updatedCommentsDto;
        }
    }
}
