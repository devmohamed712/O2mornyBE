using FluentValidation;

namespace O2morny.Application.Features.Account
{
    public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
    {
        public UpdateAccountCommandValidator()
        {
            RuleFor(x => x.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Name is required")
                .MaximumLength(256)
                .WithMessage("Name must not exceed 256 characters");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                 .Must(x => x <= DateTime.UtcNow.AddYears(-18)).WithMessage("Age must be at least 18");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("City is required");

            RuleFor(x => x.Address)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Address is required");

            When(x => x.ProfilePictureFile != null, () =>
            {
                RuleFor(x => x.ProfilePictureFile!)
                    .ChildRules(file =>
                    {
                        file.RuleFor(x => x.FileStream)
                            .NotNull()
                            .WithMessage("Profile picture stream is required");

                        file.RuleFor(x => x.FileName)
                            .NotEmpty()
                            .WithMessage("Profile picture file name is required");
                    });
            });
        }
    }
}
