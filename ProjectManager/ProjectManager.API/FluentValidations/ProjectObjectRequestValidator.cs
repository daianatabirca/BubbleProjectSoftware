﻿using FluentValidation;
using ProjectManager.DomainModel.Models.Requests;

namespace ProjectManager.API.FluentValidations
{
    public class ProjectObjectRequestValidator : AbstractValidator<ProjectObjectRequest>
    {
        public ProjectObjectRequestValidator() 
        {
            RuleFor(po => po.Title).NotEmpty().WithMessage("Project Object Title is required.");
            RuleFor(po => po.StatusId).NotEmpty().WithMessage("Project Object Status Id is required.");
            RuleFor(po => po.ProjectId).NotEmpty().WithMessage("Project Id is required.");
            RuleFor(po => po.ProjectObjectTypeId).NotEmpty().WithMessage("Project Object Type Id is required.");
            RuleFor(po => po.CreatedAt).NotEmpty().WithMessage("Project Object Creation Time is required.");
            RuleFor(po => po.CreatedBy).NotEmpty().WithMessage("Project Object CreationBy field is required.");
            RuleFor(po => po.Description).NotEmpty().WithMessage("Project Object Description is required.");

            RuleFor(po => po.Title).MaximumLength(50).WithMessage("Title < 50 characters.");
            RuleFor(po => po.Description).MaximumLength(500).WithMessage("Description < 500 characters.");

            RuleFor(po => po.SprintNumber).GreaterThan(0);
        }
    }
}
