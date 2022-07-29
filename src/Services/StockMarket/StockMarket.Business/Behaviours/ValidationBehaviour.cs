using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockMarket.Business.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<TRequest> _logger;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators, ILogger<TRequest> logger)
        {
            _validators = validators;
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                if (_validators.Any())
                {
                    var context = new ValidationContext<TRequest>(request);

                    var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                    var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                    if (failures.Count != 0)
                        throw new ValidationException(failures);
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            return await next();
        }
    }
}
