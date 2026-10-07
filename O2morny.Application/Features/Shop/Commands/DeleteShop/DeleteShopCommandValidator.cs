using FluentValidation;

namespace O2morny.Application.Features.Shop
{
    public class DeleteShopCommandValidator : AbstractValidator<DeleteShopCommand>
    {
        public DeleteShopCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required");
        }
    }
}
