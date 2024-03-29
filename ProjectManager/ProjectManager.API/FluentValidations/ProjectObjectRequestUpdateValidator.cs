using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class ProjectObjectRequestUpdateValidator : AbstractValidator<ProjectObjectRequestUpdate>
    {
        public ProjectObjectRequestUpdateValidator()
        {
            RuleFor(po => po.Title).NotEmpty().WithMessage("Project Object Title is required.");
            RuleFor(po => po.UpdatedAt).NotEmpty().WithMessage("Project Object Update Time is required.");
            RuleFor(po => po.UpdatedBy).NotEmpty().WithMessage("Project Object UpdatedBy field is required.");
            RuleFor(po => po.Description).NotEmpty().WithMessage("Project Object Description is required.");

            RuleFor(po => po.Title).MaximumLength(50).WithMessage("Title < 50 characters.");
            RuleFor(po => po.Description).MaximumLength(500).WithMessage("Description < 500 characters.");

            RuleFor(po => po.SprintNumber).GreaterThan(0);
        }
    }
}
