using FluentValidation;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class CreateServiceProviderReviewCommandValidator : AbstractValidator<CreateServiceProviderReviewCommand>
    {
        public CreateServiceProviderReviewCommandValidator()
        {
            RuleFor(x => x.ServiceProviderProfileId)
                .NotEmpty()
                .WithMessage("Service provider is required.");

            RuleFor(x => x.ClientAccountId)
                .NotEmpty()
                .WithMessage("Client account is required.");

            RuleFor(x => x.Rating)
                .Must(x => x >= 0.5m &&
                           x <= 5m &&
                           x * 2 == decimal.Truncate(x * 2))
                .WithMessage("Rating must be between 0.5 and 5 in increments of 0.5.");

            RuleFor(x => x.Review)
                .NotEmpty()
                .WithMessage("Review is required.")
                .MaximumLength(4000)
                .WithMessage("Review must not exceed 4000 characters.");
        }
    }
}
