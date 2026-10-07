using FluentValidation;

namespace O2morny.Application.Features.Shop
{
    public class GetShopByIdQueryValidator : AbstractValidator<GetShopByIdQuery>
    {
        public GetShopByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required");
        }
    }
}
