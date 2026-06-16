using FluentValidation;

namespace O2morny.Application.Features.City
{
    public class UpdateCityCommandValidator : AbstractValidator<UpdateCityCommand>
    {
        public UpdateCityCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required");

            RuleFor(x => x.EnName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("EnName is required")
                .MaximumLength(256).WithMessage("EnName must not exceed 256 characters");

            RuleFor(x => x.ArName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("ArName is required")
                .MaximumLength(256).WithMessage("ArName must not exceed 256 characters");
        }
    }
}
