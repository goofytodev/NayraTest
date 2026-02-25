namespace NayraBase.Application.DTOs.Auth;

/// <summary>
/// DTO para cambio de contraseña
/// </summary>
public class ChangePasswordDto
{
    public string CurrentPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}