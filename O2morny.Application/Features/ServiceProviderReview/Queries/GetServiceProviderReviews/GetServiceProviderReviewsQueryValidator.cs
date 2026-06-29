using FluentValidation;

namespace O2morny.Application.Features.ServiceProviderReview
{
    public class GetServiceProviderReviewsQueryValidator : AbstractValidator<GetServiceProviderReviewsQuery>
    {
        public GetServiceProviderReviewsQueryValidator()
        {
            RuleFor(x => x.Page)
                .NotNull().WithMessage("Page is required");

            RuleFor(x => x.PageSize)
                .NotNull().WithMessage("Page size is required");
        }
    }
}
