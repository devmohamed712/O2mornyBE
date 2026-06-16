using FluentValidation;
using O2morny.Application.Common.Validators;

namespace O2morny.Application.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidEgyptianNationalId<T>(
            this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(NationalIdValidator.BeValidEgyptianNationalId)
                .WithMessage("Invalid Egyptian national id");
        }
    }
}