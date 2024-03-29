using AutoMapper;
using ProjectManager.DomainModel.Models.Requests;
using ProjectManager.DomainModel.Models.Responses;
using ProjectManager.Repository.Entities;
using ProjectManager.Repository.Repositories;

namespace ProjectManager.Services.Mappings
{
    public class CommentsService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IProjectObjectRepository _projectObjectRepository;
        private readonly IMapper _mapper;

        public CommentsService(ICommentsRepository commentsRepository, IMapper mapper, IProjectObjectRepository projectObjectRepository)
        {
            _commentsRepository = commentsRepository ?? throw new ArgumentNullException(nameof(commentsRepository));
            _projectObjectRepository = projectObjectRepository ?? throw new ArgumentNullException(nameof(projectObjectRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _projectObjectRepository = projectObjectRepository;
        }

        public async Task<CommentsRequestPatch?> AddPatchAsync(int commentsId)
        {
            var existingComment = await GetCommentsByIdAsync(commentsId);

            if (existingComment == null)
            {
                return null;
            }

            var commentToPatch = _mapper.Map<CommentsRequestPatch>(existingComment);
            return commentToPatch;
        }

        public async Task<bool> CommentsExistsAsync(int commentsId)
        {
            if (!await _commentsRepository.CommentsExistsAsync(commentsId))
                return false;
            return true;
        }

        public async Task<CommentsResponse> CreateCommentsAsync(CommentsRequest commentsRequest)
        {
            var projectObjectId = commentsRequest.ProjectObjectId;

            bool doesProjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId);

            if (!doesProjectExists) //if project does not exists thorw error
            {
                throw new Exception("ProjectId does not exist!");
            }

            var commentsEntity = _mapper.Map<Comments>(commentsRequest);
            _commentsRepository.AddComments(commentsEntity);

            if (await _commentsRepository.SaveChangesAsync())
            {
                return _mapper.Map<CommentsResponse>(commentsEntity);
            }

            return null; // continue if saving failed
        }

        public async Task DeleteCommentsAsync(int commentsId)
        {
            var existingComments = await _commentsRepository.GetCommentsByIdAsync(commentsId);

            if (existingComments == null)
            {
                throw new Exception("Project not found");
            }

            await _commentsRepository.DeleteAsync(existingComments);
        }

        public async Task<IEnumerable<CommentsResponse>> GetCommentsAsync()
        {
            var comments = await _commentsRepository.GetCommentsAsync();
            return _mapper.Map<IEnumerable<CommentsResponse>>(comments);
        }

        public async Task<CommentsResponse?> GetCommentsByIdAsync(int commentsId)
        {
            var comments = await _commentsRepository.GetCommentsByIdAsync(commentsId);
            return _mapper.Map<Comments, CommentsResponse>(comments);
        }

        public async Task<CommentsResponse?> UpdateCommentsAsync(CommentsRequestUpdate commentsRequestUpdate, int commentsId)
        {
            var projectObjectId = commentsRequestUpdate.ProjectObjectId;

            bool doesProjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId);

            if (!doesProjectExists) //if project does not exists thorw error
            {
                throw new Exception("ProjectId does not exist!");
            }

            var existingComments = await _commentsRepository.GetCommentsByIdAsync(commentsId);

            if (existingComments == null)
            {
                return null;
            }

            var intermediateComments = _mapper.Map(commentsRequestUpdate, existingComments);

            var updatedComments = await _commentsRepository.UpdateAsync(intermediateComments);

            var updatedCommentsDto = _mapper.Map<Comments, CommentsResponse>(updatedComments);

            return updatedCommentsDto;
        }

        public async Task<CommentsResponse?> UpdateCommentsPatchAsync(CommentsRequestPatch commentsRequestUpdate, int commentsId)
        {
            var projectObjectId = commentsRequestUpdate.ProjectObjectId;

            bool doesProjectExists = await _projectObjectRepository.ProjectObjectExistsAsync(projectObjectId);

            if (!doesProjectExists) //if project does not exists thorw error
            {
                throw new Exception("ProjectId does not exist!");
            }

            var existingComments = await _commentsRepository.GetCommentsByIdAsync(commentsId);

            if (existingComments == null)
            {
                return null;
            }

            var intermediateComments = _mapper.Map(commentsRequestUpdate, existingComments);

            var updatedComments = await _commentsRepository.UpdateAsync(intermediateComments);

            var updatedCommentsDto = _mapper.Map<Comments, CommentsResponse>(updatedComments);

            return updatedCommentsDto;
        }
    }
}
