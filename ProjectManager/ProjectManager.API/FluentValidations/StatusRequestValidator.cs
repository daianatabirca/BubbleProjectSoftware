using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class StatusRequestValidator : AbstractValidator<StatusRequest>
    {
        public StatusRequestValidator() 
        {
            RuleFor(x => x.Type).NotEmpty().WithMessage("Status type is required.");
            RuleFor(x => x.Type).MaximumLength(20).WithMessage("Type < 20 characters.");
        }
    }
}
