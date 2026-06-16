using FluentValidation;

namespace O2morny.Application.Features.Country
{
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("Id is required");
        }
    }
}
