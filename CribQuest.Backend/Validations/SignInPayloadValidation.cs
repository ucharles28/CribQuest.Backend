using FluentValidation;

namespace CribQuest.Backend.Validations;

public class SignInPayloadValidation : AbstractValidator<SignInPayload>
{
    public SignInPayloadValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email field is required");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password field is required");
    }
}