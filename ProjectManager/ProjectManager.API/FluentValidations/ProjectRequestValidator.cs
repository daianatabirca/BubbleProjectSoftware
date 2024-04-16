using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class ProjectRequestValidator : AbstractValidator<ProjectRequest>
    {
        public ProjectRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Description).MaximumLength(250).WithMessage("Description < 250 characters.");
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage("StartDate < EndDate");
        }
    }
}