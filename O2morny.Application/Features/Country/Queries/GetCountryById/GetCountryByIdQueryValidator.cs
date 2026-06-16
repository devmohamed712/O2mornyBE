using FluentValidation;

namespace O2morny.Application.Features.Country
{
    public class GetCountryByIdQueryValidator : AbstractValidator<GetCountryByIdQuery>
    {
        public GetCountryByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required");
        }
    }
}
