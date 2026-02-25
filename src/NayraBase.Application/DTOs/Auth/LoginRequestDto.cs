namespace NayraBase.Application.DTOs.Auth;

/// <summary>
/// DTO para solicitud de login
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// Username o email del usuario
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Contraseña
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Recordar sesión (opcional)
    /// </summary>
    public bool RememberMe { get; set; } = false;
}
