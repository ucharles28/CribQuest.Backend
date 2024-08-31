using FluentValidation;

namespace CribQuest.Backend.Validations;

public class SignUpPayloadValidation : AbstractValidator<SignUpPayload>
{
    public SignUpPayloadValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .WithMessage("Email field is required");
        
        RuleFor(x => x.Firstname)
            .NotEmpty()
            .NotNull()
            .WithMessage("Firstname field is required");
        
        RuleFor(x => x.Lastname)
            .NotEmpty()
            .NotNull()
            .WithMessage("Lastname field is required");
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .WithMessage("Password field is required");
        
        RuleFor(x => x.Phone)
            .NotEmpty()
            .NotNull()
            .WithMessage("Phone number field is required");
    }
}