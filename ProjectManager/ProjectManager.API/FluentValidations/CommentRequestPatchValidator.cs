using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class CommentRequestPatchValidator : AbstractValidator<CommentRequestPatch>
    {
        public CommentRequestPatchValidator() 
        {
            RuleFor(c => c.CommentArea).MaximumLength(500).WithMessage("Comment area can contain maximum 500 characters.");
        }
    }
}