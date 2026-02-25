using NayraBase.Domain.Enums;

namespace NayraBase.Application.DTOs.Auth;

/// <summary>
/// DTO para registro de nuevo usuario
/// </summary>
public class RegisterRequestDto
{
    // Datos de persona
    public string Nombres { get; set; } = string.Empty;
    public string ApellidoPaterno { get; set; } = string.Empty;
    public string? ApellidoMaterno { get; set; }
    public TipoDocumento TipoDocumento { get; set; }
    public string NumeroDocumento { get; set; } = string.Empty;
    public string? Telefono { get; set; }
    public string Email { get; set; } = string.Empty;

    // Datos de usuario
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}