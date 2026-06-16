using FluentValidation;

namespace O2morny.Application.Features.City
{
    public class DeleteCityCommandValidator : AbstractValidator<DeleteCityCommand>
    {
        public DeleteCityCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required");
        }
    }
}
