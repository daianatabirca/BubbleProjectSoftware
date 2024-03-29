using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class ProjectObjectTypeRequestValidator : AbstractValidator<ProjectObjectTypeRequest>
    {
        public ProjectObjectTypeRequestValidator() 
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Project Object type is required.");
            RuleFor(x => x.Type).MaximumLength(20).WithMessage("Type < 20 characters.");
        }
    }
}
