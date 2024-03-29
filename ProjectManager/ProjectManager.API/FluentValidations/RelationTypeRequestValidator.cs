using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class RelationTypeRequestValidator : AbstractValidator<RelationTypeRequest>
    {
        public RelationTypeRequestValidator()
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Relation type is required.");
            RuleFor(x => x.Type).MaximumLength(20).WithMessage("Type < 20 characters.");
        }
    }
}
