using FluentValidation;
using NayraBase.Application.DTOs.Auth;

namespace NayraBase.Application.Validators.FluentValidation;

/// <summary>
/// Validador para RegisterRequestDto
/// </summary>
public class RegisterRequestValidator : AbstractValidator<RegisterRequestDto>
{
    public RegisterRequestValidator()
    {
        // Persona
        RuleFor(x => x.Nombres)
            .NotEmpty().WithMessage("Los nombres son requeridos")
            .MaximumLength(100).WithMessage("Los nombres no pueden exceder 100 caracteres");

        RuleFor(x => x.ApellidoPaterno)
            .NotEmpty().WithMessage("El apellido paterno es requerido")
            .MaximumLength(100).WithMessage("El apellido paterno no puede exceder 100 caracteres");

        RuleFor(x => x.ApellidoMaterno)
            .MaximumLength(100).WithMessage("El apellido materno no puede exceder 100 caracteres")
            .When(x => !string.IsNullOrEmpty(x.ApellidoMaterno));

        RuleFor(x => x.NumeroDocumento)
            .NotEmpty().WithMessage("El número de documento es requerido")
            .MaximumLength(20).WithMessage("El número de documento no puede exceder 20 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido")
            .EmailAddress().WithMessage("El email no es válido")
            .MaximumLength(100).WithMessage("El email no puede exceder 100 caracteres");

        // Usuario
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("El username es requerido")
            .MinimumLength(3).WithMessage("El username debe tener al menos 3 caracteres")
            .MaximumLength(50).WithMessage("El username no puede exceder 50 caracteres")
            .Matches("^[a-zA-Z0-9._-]+$").WithMessage("El username solo puede contener letras, números, puntos, guiones y guiones bajos");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
            .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula")
            .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula")
            .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número")
            .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Las contraseñas no coinciden");
    }
}