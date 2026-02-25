namespace NayraBase.Application.DTOs.Auth;

/// <summary>
/// DTO para respuesta de login exitoso
/// </summary>
public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = "Bearer";
    public UsuarioInfoDto Usuario { get; set; } = null!;
}
