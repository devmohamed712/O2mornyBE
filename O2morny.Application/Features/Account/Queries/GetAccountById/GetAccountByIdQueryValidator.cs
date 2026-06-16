using FluentValidation;
using O2morny.Application.Features.Account;

namespace O2morny.Application.Features.Account
{
    public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
    {
        public GetAccountByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");
        }
    }
}
