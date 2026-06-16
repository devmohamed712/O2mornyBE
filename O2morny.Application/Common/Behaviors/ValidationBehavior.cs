using FluentValidation;
using MediatR;
using O2morny.Application.Common.Exceptions;

namespace O2morny.Application.Common.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    Dictionary<string, string[]> errors = validationResults.SelectMany(r => r.Errors).GroupBy(x => x.PropertyName).ToDictionary(
                       g => g.Key,
                       g => g.Select(x => x.ErrorMessage).ToArray()
                   );

                    throw new AppValidationException(errors);
                }
            }

            return await next();
        }
    }
}
