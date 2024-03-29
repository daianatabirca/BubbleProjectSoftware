using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class CommentsRequestValidator : AbstractValidator<CommentsRequest>
    {
        public CommentsRequestValidator()
        {
            RuleFor(c => c.InsertDate).NotEmpty().WithMessage("Comments updated date field is required.");
            RuleFor(c => c.Username).NotEmpty().WithMessage("Username is required.");
            RuleFor(c => c.CommentArea).NotEmpty().WithMessage("Comment area is required.");

            RuleFor(c => c.CommentArea).MaximumLength(500).WithMessage("Comment area can contain maximum 500 characters.");
        }
    }
}
