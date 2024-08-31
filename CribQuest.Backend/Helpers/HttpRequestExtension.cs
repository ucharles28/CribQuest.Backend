using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CribQuest.Backend.Helpers;

public static class HttpRequestExtension
{
    /// @param T
    /// 
    /// @return A ValidatableRequest with the
    public static async Task<ValidatableRequest<T>> GetJsonBody<T, TV>(this T payload)
        where TV : AbstractValidator<T>, new()
    {
        var validator = new TV();
        var validationResult = await validator.ValidateAsync(payload);

        // Returns a validatableRequest object that is not valid.
        if (!validationResult.IsValid)
        {
            return new ValidatableRequest<T>
            {
                Value = payload,
                IsValid = false,
                Errors = validationResult.Errors
            };
        }

        return new ValidatableRequest<T>
        {
            Value = payload,
            IsValid = true
        };
    }
}

public static class ValidationExtensions
{
    /// @param ValidatableRequest<T >
    /// 
    /// @return The instance which represents the
    public static BadRequestObjectResult ToBadRequest<T>(this ValidatableRequest<T> request)
    {
        return new BadRequestObjectResult(request.Errors.Select(e => new
        {
            // Field = e.PropertyName,
            Message = e.ErrorMessage,
            Success = false
        }).Distinct().FirstOrDefault());
    }
}

public class ValidatableRequest<T>
{
    public T Value { get; set; }
    public bool IsValid { get; set; }
    public IList<ValidationFailure> Errors { get; set; }
}