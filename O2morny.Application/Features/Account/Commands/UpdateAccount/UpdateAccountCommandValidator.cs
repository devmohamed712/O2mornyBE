using FluentValidation;
using O2morny.Domain.Common.Enums;

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

            RuleFor(x => x.Role)
                .NotEmpty()
                .WithMessage("Role is required")
                .Must(x => x == nameof(AccountRole.Client) ||
                           x == nameof(AccountRole.ServiceProvider))
                .WithMessage("Invalid role");

            RuleFor(x => x.ServiceProviderExperienceYears)
                .NotNull()
                .WithMessage("Years of experience is required.")
                .GreaterThan(0)
                .WithMessage("Years of experience must be greater than 0.")
                .When(x => x.Role == nameof(AccountRole.ServiceProvider));

            RuleFor(x => x.ServiceProviderDescription)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(4000)
                .WithMessage("Description must not exceed 4000 characters.")
                .When(x => x.Role == nameof(AccountRole.ServiceProvider));
        }
    }
}
