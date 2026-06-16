using FluentValidation;
using O2morny.Application.Common.Extensions;

namespace O2morny.Application.Features.Account
{
    public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
    {
        public CreateAccountCommandValidator()
        {
            RuleFor(x => x.Name)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Name is required")
                .MaximumLength(256).WithMessage("Name must not exceed 256 characters");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required")
                 .Must(x => x <= DateTime.UtcNow.AddYears(-18)).WithMessage("Age must be at least 18");

            RuleFor(x => x.CityId)
                .NotEmpty().WithMessage("City is required");

            RuleFor(x => x.Address)
                .Must(x => !string.IsNullOrWhiteSpace(x))
                .WithMessage("Address is required");

            RuleFor(x => x.NationalId)
                .ValidEgyptianNationalId();

            RuleFor(x => x.IsAcceptTerms)
                .Equal(true).WithMessage("Terms must be accepted");

            RuleFor(x => x.IsAcceptPrivacy)
                .Equal(true).WithMessage("Privacy policy must be accepted");

            RuleFor(x => x.ProfilePictureFile)
                .NotNull().WithMessage("Profile picture is required")
                .DependentRules(() =>
                {
                    RuleFor(x => x.ProfilePictureFile.FileStream)
                        .NotNull().WithMessage("Profile picture stream is required");

                    RuleFor(x => x.ProfilePictureFile.FileName)
                        .NotEmpty().WithMessage("Profile picture file name is required");
                });

            RuleFor(x => x.NationalIdPictureFile)
                .NotNull().WithMessage("National id picture is required")
                .DependentRules(() =>
                {
                    RuleFor(x => x.NationalIdPictureFile.FileStream)
                        .NotNull().WithMessage("National id picture stream is required");

                    RuleFor(x => x.NationalIdPictureFile.FileName)
                        .NotEmpty().WithMessage("National id picture file name is required");
                });

            RuleFor(x => x.Role)
                .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Role is required");
        }
    }
}
