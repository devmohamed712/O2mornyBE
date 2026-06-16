using FluentValidation;

namespace O2morny.Application.Features.City
{
    public class CreateCityCommandValidator : AbstractValidator<CreateCityCommand>
    {
        public CreateCityCommandValidator()
        {
            RuleFor(x => x.CountryId)
                .NotNull().WithMessage("Country id is required");

            RuleFor(x => x.EnName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("EnName is required")
                .MaximumLength(256).WithMessage("EnName must not exceed 256 characters");

            RuleFor(x => x.ArName)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("ArName is required")
                .MaximumLength(256).WithMessage("ArName must not exceed 256 characters");
        }
    }
}
