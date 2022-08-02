using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StockMarket.Business.Exceptions
{
    /// <summary>
    /// class for validation exception
    /// </summary>
    public class ValidationException : ApplicationException
    {
        /// <summary>
        /// constructor for validationexception
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// parameter constructor for validationexception
        /// </summary>
        /// <param name="failures"></param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
