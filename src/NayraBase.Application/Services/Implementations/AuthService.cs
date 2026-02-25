using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using NayraBase.Application.Common.Results;
using NayraBase.Application.DTOs.Auth;
using NayraBase.Application.Services.Interfaces;
using NayraBase.Domain.Entities;
using NayraBase.Infrastructure.Security;
using NayraBase.Infrastructure.UnitOfWork;

namespace NayraBase.Application.Services.Implementations;

/// <summary>
/// Implementación del servicio de autenticación
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtTokenGenerator _jwtGenerator;

    public AuthService(
        IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher();
        _jwtGenerator = new JwtTokenGenerator(configuration);
    }

    public async Task<ServiceResult<LoginResponseDto>> LoginAsync(LoginRequestDto request)
    {
        // Buscar usuario
        var usuario = await _unitOfWork.Usuarios.GetByUsernameWithPersonaAsync(request.Username);

        if (usuario == null || !usuario.Activo)
        {
            return ServiceResult<LoginResponseDto>.FailureResult("Usuario o contraseña incorrectos");
        }

        // Verificar lockout
        if (usuario.LockoutEnabled && usuario.LockoutEnd.HasValue && usuario.LockoutEnd.Value > DateTime.UtcNow)
        {
            var minutosRestantes = (int)(usuario.LockoutEnd.Value - DateTime.UtcNow).TotalMinutes;
            return ServiceResult<LoginResponseDto>.FailureResult(
                $"Cuenta bloqueada. Intente nuevamente en {minutosRestantes} minutos"
            );
        }

        // Verificar contraseña
        var passwordValid = _passwordHasher.VerifyPassword(usuario.PasswordHash, request.Password);

        if (!passwordValid)
        {
            await _unitOfWork.Usuarios.IncrementAccessFailedCountAsync(usuario.Id);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult<LoginResponseDto>.FailureResult("Usuario o contraseña incorrectos");
        }

        // Login exitoso
        await _unitOfWork.Usuarios.ResetAccessFailedCountAsync(usuario.Id);
        await _unitOfWork.Usuarios.UpdateLastAccessAsync(usuario.Id);

        // Generar tokens
        var accessToken = _jwtGenerator.GenerateToken(usuario);
        var refreshToken = GenerateRefreshToken();

        // Guardar refresh token
        var refreshTokenEntity = new RefreshToken
        {
            UsuarioId = usuario.Id,
            Token = refreshToken,
            FechaExpiracion = DateTime.UtcNow.AddDays(
                int.Parse(_configuration["RefreshToken:ExpirationDays"] ?? "7")
            ),
            IpAddress = null, // Agregar IP si está disponible
            UserAgent = null  // Agregar UserAgent si está disponible
        };

        await _unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);
        await _unitOfWork.SaveChangesAsync();

        // Crear respuesta
        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60") * 60,
            Usuario = new UsuarioInfoDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                EmailConfirmed = usuario.EmailConfirmed,
                DebeCambiarPassword = usuario.DebeCambiarPassword,
                Persona = new PersonaBasicaDto
                {
                    Id = usuario.Persona.Id,
                    Nombres = usuario.Persona.Nombres,
                    ApellidoPaterno = usuario.Persona.ApellidoPaterno,
                    ApellidoMaterno = usuario.Persona.ApellidoMaterno
                }
            }
        };

        return ServiceResult<LoginResponseDto>.SuccessResult(response);
    }

    public async Task<ServiceResult<LoginResponseDto>> RegisterAsync(RegisterRequestDto request)
    {
        // Verificar username disponible
        if (await _unitOfWork.Usuarios.ExistsUsernameAsync(request.Username))
        {
            return ServiceResult<LoginResponseDto>.FailureResult("El username ya está en uso");
        }

        // Verificar email disponible
        if (await _unitOfWork.Usuarios.ExistsEmailAsync(request.Email))
        {
            return ServiceResult<LoginResponseDto>.FailureResult("El email ya está registrado");
        }

        // Verificar documento disponible
        if (await _unitOfWork.Personas.ExistsNumeroDocumentoAsync(request.NumeroDocumento))
        {
            return ServiceResult<LoginResponseDto>.FailureResult("El número de documento ya está registrado");
        }

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Crear persona
            var persona = new Persona
            {
                Nombres = request.Nombres,
                ApellidoPaterno = request.ApellidoPaterno,
                ApellidoMaterno = request.ApellidoMaterno,
                TipoDocumento = request.TipoDocumento,
                NumeroDocumento = request.NumeroDocumento,
                Email = request.Email,
                Telefono = request.Telefono,
                Estado = true
            };

            await _unitOfWork.Personas.AddAsync(persona);
            await _unitOfWork.SaveChangesAsync();

            // Crear usuario
            var usuario = new Usuario
            {
                PersonaId = persona.Id,
                Username = request.Username,
                NormalizedUsername = TextNormalizer.NormalizeUsername(request.Username),
                Email = request.Email,
                NormalizedEmail = TextNormalizer.NormalizeEmail(request.Email),
                PasswordHash = _passwordHasher.HashPassword(request.Password),
                SecurityStamp = SecurityStampGenerator.Generate(),
                ConcurrencyStamp = SecurityStampGenerator.GenerateConcurrencyStamp(),
                EmailConfirmed = false,
                DebeCambiarPassword = false,
                Activo = true
            };

            await _unitOfWork.Usuarios.AddAsync(usuario);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            // Hacer login automático después del registro
            var loginRequest = new LoginRequestDto
            {
                Username = request.Username,
                Password = request.Password
            };

            return await LoginAsync(loginRequest);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return ServiceResult<LoginResponseDto>.FailureResult($"Error al registrar usuario: {ex.Message}");
        }
    }

    public async Task<ServiceResult<LoginResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request)
    {
        var refreshToken = await _unitOfWork.RefreshTokens.GetByTokenAsync(request.RefreshToken);

        if (refreshToken == null || !refreshToken.EstaActivo)
        {
            return ServiceResult<LoginResponseDto>.FailureResult("Refresh token inválido o expirado");
        }

        var usuario = await _unitOfWork.Usuarios.GetByIdWithPersonaAsync(refreshToken.UsuarioId);

        if (usuario == null || !usuario.Activo)
        {
            return ServiceResult<LoginResponseDto>.FailureResult("Usuario no encontrado o inactivo");
        }

        // Revocar token anterior
        await _unitOfWork.RefreshTokens.RevokeTokenAsync(request.RefreshToken);

        // Generar nuevos tokens
        var accessToken = _jwtGenerator.GenerateToken(usuario);
        var newRefreshToken = GenerateRefreshToken();

        // Guardar nuevo refresh token
        var newRefreshTokenEntity = new RefreshToken
        {
            UsuarioId = usuario.Id,
            Token = newRefreshToken,
            FechaExpiracion = DateTime.UtcNow.AddDays(
                int.Parse(_configuration["RefreshToken:ExpirationDays"] ?? "7")
            ),
            IpAddress = null,
            UserAgent = null
        };

        await _unitOfWork.RefreshTokens.AddAsync(newRefreshTokenEntity);
        await _unitOfWork.SaveChangesAsync();

        var response = new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            ExpiresIn = int.Parse(_configuration["Jwt:ExpirationMinutes"] ?? "60") * 60,
            Usuario = new UsuarioInfoDto
            {
                Id = usuario.Id,
                Username = usuario.Username,
                Email = usuario.Email,
                EmailConfirmed = usuario.EmailConfirmed,
                DebeCambiarPassword = usuario.DebeCambiarPassword,
                Persona = new PersonaBasicaDto
                {
                    Id = usuario.Persona.Id,
                    Nombres = usuario.Persona.Nombres,
                    ApellidoPaterno = usuario.Persona.ApellidoPaterno,
                    ApellidoMaterno = usuario.Persona.ApellidoMaterno
                }
            }
        };

        return ServiceResult<LoginResponseDto>.SuccessResult(response);
    }

    public async Task<ServiceResult> LogoutAsync(string refreshToken)
    {
        await _unitOfWork.RefreshTokens.RevokeTokenAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.SuccessResult("Sesión cerrada correctamente");
    }

    public async Task<ServiceResult> ChangePasswordAsync(int userId, ChangePasswordDto request)
    {
        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(userId);

        if (usuario == null)
        {
            return ServiceResult.FailureResult("Usuario no encontrado");
        }

        // Verificar contraseña actual
        var passwordValid = _passwordHasher.VerifyPassword(usuario.PasswordHash, request.CurrentPassword);

        if (!passwordValid)
        {
            return ServiceResult.FailureResult("La contraseña actual es incorrecta");
        }

        // Actualizar contraseña
        usuario.PasswordHash = _passwordHasher.HashPassword(request.NewPassword);
        usuario.SecurityStamp = SecurityStampGenerator.Generate();
        usuario.DebeCambiarPassword = false;

        await _unitOfWork.Usuarios.UpdateAsync(usuario);

        // Revocar todos los tokens del usuario
        await _unitOfWork.RefreshTokens.RevokeAllUserTokensAsync(userId);

        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.SuccessResult("Contraseña actualizada correctamente");
    }

    public async Task<bool> IsUsernameAvailableAsync(string username)
    {
        return !await _unitOfWork.Usuarios.ExistsUsernameAsync(username);
    }

    public async Task<bool> IsEmailAvailableAsync(string email)
    {
        return !await _unitOfWork.Usuarios.ExistsEmailAsync(email);
    }

    #region Helpers

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    #endregion
}