namespace NayraBase.Application.DTOs.Auth;

/// <summary>
/// DTO para solicitud de renovación de token
/// </summary>
public class RefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = string.Empty;
}