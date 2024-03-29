using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class CommentsRequestPatchValidator : AbstractValidator<CommentsRequestPatch>
    {
        public CommentsRequestPatchValidator() 
        {
            RuleFor(c => c.CommentArea).MaximumLength(500).WithMessage("Comment area can contain maximum 500 characters.");
        }
    }
}