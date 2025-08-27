using FluentValidation.Results;
using KopiBudget.Domain.Abstractions;
using System.Net;

namespace KopiBudget.Application.Extensions
{
    public static class ValidationResultExtensions
    {
        #region Public Methods

        public static List<Error> ToErrorList(this ValidationResult result)
        {
            return result.Errors
                         .Select(e => new Error(e.PropertyName, e.ErrorMessage, ((int)HttpStatusCode.BadRequest)))
                         .ToList();
        }

        #endregion Public Methods
    }
}