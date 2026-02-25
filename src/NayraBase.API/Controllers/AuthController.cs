using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NayraBase.Application.DTOs.Auth;
using NayraBase.Application.Services.Interfaces;

namespace NayraBase.API.Controllers;

/// <summary>
/// Controller de autenticación
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Login de usuario
    /// </summary>
    /// <param name="request">Credenciales de usuario</param>
    /// <returns>Tokens de acceso y refresh</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        var result = await _authService.LoginAsync(request);

        if (!result.Success)
        {
            return Unauthorized(new
            {
                message = result.Message,
                errors = result.Errors
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Registro de nuevo usuario
    /// </summary>
    /// <param name="request">Datos del nuevo usuario</param>
    /// <returns>Tokens de acceso después del registro</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
    {
        var result = await _authService.RegisterAsync(request);

        if (!result.Success)
        {
            return BadRequest(new
            {
                message = result.Message,
                errors = result.Errors
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Renovar token de acceso usando refresh token
    /// </summary>
    /// <param name="request">Refresh token</param>
    /// <returns>Nuevos tokens</returns>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _authService.RefreshTokenAsync(request);

        if (!result.Success)
        {
            return Unauthorized(new
            {
                message = result.Message,
                errors = result.Errors
            });
        }

        return Ok(result.Data);
    }

    /// <summary>
    /// Cerrar sesión (revocar refresh token)
    /// </summary>
    /// <param name="request">Refresh token a revocar</param>
    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenRequestDto request)
    {
        var result = await _authService.LogoutAsync(request.RefreshToken);

        if (!result.Success)
        {
            return BadRequest(new
            {
                message = result.Message,
                errors = result.Errors
            });
        }

        return Ok(new { message = result.Message });
    }

    /// <summary>
    /// Cambiar contraseña del usuario autenticado
    /// </summary>
    /// <param name="request">Contraseña actual y nueva</param>
    [HttpPost("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto request)
    {
        // Obtener ID del usuario autenticado desde el token JWT
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            return Unauthorized(new { message = "Token inválido" });
        }

        var result = await _authService.ChangePasswordAsync(userId, request);

        if (!result.Success)
        {
            return BadRequest(new
            {
                message = result.Message,
                errors = result.Errors
            });
        }

        return Ok(new { message = result.Message });
    }

    /// <summary>
    /// Verificar si un username está disponible
    /// </summary>
    /// <param name="username">Username a verificar</param>
    [HttpGet("check-username/{username}")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckUsername(string username)
    {
        var available = await _authService.IsUsernameAvailableAsync(username);

        return Ok(new
        {
            username,
            available
        });
    }

    /// <summary>
    /// Verificar si un email está disponible
    /// </summary>
    /// <param name="email">Email a verificar</param>
    [HttpGet("check-email")]
    [AllowAnonymous]
    public async Task<IActionResult> CheckEmail([FromQuery] string email)
    {
        var available = await _authService.IsEmailAvailableAsync(email);

        return Ok(new
        {
            email,
            available
        });
    }

    /// <summary>
    /// Obtener información del usuario autenticado
    /// </summary>
    [HttpGet("me")]
    [Authorize]
    public IActionResult GetCurrentUser()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        var username = User.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        return Ok(new
        {
            userId = userId != null ? int.Parse(userId) : 0,
            username,
            email,
            claims = User.Claims.Select(c => new { c.Type, c.Value })
        });
    }

    /// <summary>
    /// Endpoint de prueba (sin autenticación)
    /// </summary>
    [HttpGet("test")]
    [AllowAnonymous]
    public IActionResult Test()
    {
        return Ok(new
        {
            message = "API funcionando correctamente",
            timestamp = DateTime.UtcNow,
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        });
    }
}