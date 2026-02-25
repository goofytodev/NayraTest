using FluentValidation;
using NayraBase.Application.DTOs.Auth;

namespace NayraBase.Application.Validators.FluentValidation;

/// <summary>
/// Validador para ChangePasswordDto
/// </summary>
public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("La contraseña actual es requerida");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("La nueva contraseña es requerida")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres")
            .Matches("[A-Z]").WithMessage("La contraseña debe contener al menos una letra mayúscula")
            .Matches("[a-z]").WithMessage("La contraseña debe contener al menos una letra minúscula")
            .Matches("[0-9]").WithMessage("La contraseña debe contener al menos un número")
            .Matches("[^a-zA-Z0-9]").WithMessage("La contraseña debe contener al menos un carácter especial")
            .NotEqual(x => x.CurrentPassword).WithMessage("La nueva contraseña debe ser diferente a la actual");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword).WithMessage("Las contraseñas no coinciden");
    }
}