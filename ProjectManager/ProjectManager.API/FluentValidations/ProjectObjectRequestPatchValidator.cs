using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class ProjectObjectRequestPatchValidator : AbstractValidator<ProjectObjectRequestPatch>
    {
        public ProjectObjectRequestPatchValidator()
        {
            RuleFor(po => po.Title).MaximumLength(50).WithMessage("Title < 50 characters.");
            RuleFor(po => po.Description).MaximumLength(500).WithMessage("Description < 500 characters.");

            RuleFor(po => po.SprintNumber).GreaterThan(0);
        }
    }
}
