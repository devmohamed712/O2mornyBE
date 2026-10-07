using FluentValidation;

namespace O2morny.Application.Features.Shop
{
    public class GetShopsQueryValidator : AbstractValidator<GetShopsQuery>
    {
        public GetShopsQueryValidator()
        {
            RuleFor(x => x.Page)
                .NotNull().WithMessage("Page is required");

            RuleFor(x => x.PageSize)
                .NotNull().WithMessage("Page size is required");
        }
    }
}
