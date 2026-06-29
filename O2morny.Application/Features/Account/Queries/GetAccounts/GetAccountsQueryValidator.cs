using FluentValidation;

namespace O2morny.Application.Features.Account
{
    public class GetAccountsQueryValidator : AbstractValidator<GetAccountsQuery>
    {
        public GetAccountsQueryValidator()
        {
            RuleFor(x => x.Page)
                .NotNull().WithMessage("Page is required");

            RuleFor(x => x.PageSize)
                .NotNull().WithMessage("Page size is required");
        }
    }
}
