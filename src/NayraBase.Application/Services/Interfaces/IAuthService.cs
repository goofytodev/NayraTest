using NayraBase.Application.Common.Results;
using NayraBase.Application.DTOs.Auth;

namespace NayraBase.Application.Services.Interfaces;

/// <summary>
/// Servicio de autenticación
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Autentica un usuario y genera tokens
    /// </summary>
    Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginRequestDto request);

    /// <summary>
    /// Registra un nuevo usuario
    /// </summary>
    Task<ServiceResult<LoginResponseDto>> RegisterAsync(RegisterRequestDto request);

    /// <summary>
    /// Renueva el access token usando un refresh token
    /// </summary>
    Task<ServiceResult<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request);

    /// <summary>
    /// Cierra la sesión revocando el refresh token
    /// </summary>
    Task<ServiceResult> LogoutAsync(string refreshToken);

    /// <summary>
    /// Cambia la contraseña del usuario
    /// </summary>
    Task<ServiceResult> ChangePasswordAsync(int userId, ChangePasswordDto request);

    /// <summary>
    /// Verifica si un username está disponible
    /// </summary>
    Task<bool> IsUsernameAvailableAsync(string username);

    /// <summary>
    /// Verifica si un email está disponible
    /// </summary>
    Task<bool> IsEmailAvailableAsync(string email);
}