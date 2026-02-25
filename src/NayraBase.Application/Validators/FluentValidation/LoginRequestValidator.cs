using FluentValidation;
using NayraBase.Application.DTOs.Auth;

namespace NayraBase.Application.Validators.FluentValidation;

/// <summary>
/// Validador para LoginRequestDto
/// </summary>
public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El username es requerido")
            .MinimumLength(3).WithMessage("El username debe tener al menos 3 caracteres")
            .MaximumLength(50).WithMessage("El username no puede exceder 50 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres");
    }
}