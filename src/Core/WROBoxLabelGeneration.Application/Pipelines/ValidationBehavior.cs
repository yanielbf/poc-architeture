using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using WROBoxLabelGeneration.SharedKernel.Extensions;
using WROBoxLabelGeneration.SharedKernel.Logger;

namespace WROBoxLabelGeneration.Application.Pipelines
{
    public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (validators.Any())
            {
                var typeName = request.GetGenericTypeName();
                var context = new ValidationContext<TRequest>(request);

                LoggerHelper.GetLogger().LogInformation("Validating command {CommandType}", typeName);

                var validationResults = await Task.WhenAll(validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    LoggerHelper.GetLogger().LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);
                    throw new ValidationException(failures);
                }
            }

            return await next();
        }
    }
}
