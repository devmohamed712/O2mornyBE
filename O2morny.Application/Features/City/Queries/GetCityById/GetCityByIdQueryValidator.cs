using FluentValidation;

namespace O2morny.Application.Features.City
{
    public class GetCityByIdQueryValidator : AbstractValidator<GetCityByIdQuery>
    {
        public GetCityByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required");
        }
    }
}
