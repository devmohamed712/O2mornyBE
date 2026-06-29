using FluentValidation;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class GetServiceProviderReviewByIdQueryValidator : AbstractValidator<GetServiceProviderReviewByIdQuery>
    {
        public GetServiceProviderReviewByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
