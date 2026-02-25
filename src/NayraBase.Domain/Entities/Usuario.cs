//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class Usuario
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa una cuenta de usuario en el sistema
/// </summary>
public class Usuario : BaseEntity
{
    // Relación con Persona

    /// <summary>
    /// ID de la persona asociada (relación 1:1)
    /// </summary>
    public int PersonaId { get; set; }

    /// <summary>
    /// Persona asociada
    /// </summary>
    public virtual Persona Persona { get; set; } = null!;

    // Credenciales

    /// <summary>
    /// Nombre de usuario (único)
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de usuario normalizado (mayúsculas, para búsquedas)
    /// </summary>
    public string NormalizedUsername { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico (único)
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico normalizado
    /// </summary>
    public string NormalizedEmail { get; set; } = string.Empty;

    /// <summary>
    /// Indica si el email ha sido confirmado
    /// </summary>
    public bool EmailConfirmed { get; set; } = false;

    // Seguridad (Inspirado en Identity)

    /// <summary>
    /// Hash de la contraseña (PBKDF2)
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Sello de seguridad - cambia cuando se modifica la contraseña
    /// Invalida todos los tokens JWT anteriores
    /// </summary>
    public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Sello de concurrencia - para evitar actualizaciones concurrentes
    /// </summary>
    public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

    // Lockout (Bloqueo de cuenta)

    /// <summary>
    /// Indica si el bloqueo está habilitado para este usuario
    /// </summary>
    public bool LockoutEnabled { get; set; } = true;

    /// <summary>
    /// Fecha hasta la cual el usuario está bloqueado (null = no bloqueado)
    /// </summary>
    public DateTime? LockoutEnd { get; set; }

    /// <summary>
    /// Contador de intentos fallidos de login
    /// </summary>
    public int AccessFailedCount { get; set; } = 0;

    // Two-Factor Authentication (para futuro)

    /// <summary>
    /// Indica si la autenticación de dos factores está habilitada
    /// </summary>
    public bool TwoFactorEnabled { get; set; } = false;

    // Teléfono (opcional)

    /// <summary>
    /// Número de teléfono
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Indica si el teléfono ha sido confirmado
    /// </summary>
    public bool PhoneNumberConfirmed { get; set; } = false;

    // Control adicional

    /// <summary>
    /// Indica si el usuario debe cambiar su contraseña en el próximo login
    /// </summary>
    public bool DebeCambiarPassword { get; set; } = true;

    /// <summary>
    /// Fecha y hora del último acceso al sistema
    /// </summary>
    public DateTime? UltimoAcceso { get; set; }

    /// <summary>
    /// Indica si el usuario está activo
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Usuario que creó este registro
    /// </summary>
    public int? CreadoPor { get; set; }

    // Navegación

    /// <summary>
    /// Tokens de actualización asociados al usuario
    /// </summary>
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    /// <summary>
    /// Tokens de confirmación (email, reset password, etc.)
    /// </summary>
    public virtual ICollection<TokenConfirmacion> TokensConfirmacion { get; set; } = new List<TokenConfirmacion>();

    /// <summary>
    /// Asignaciones de perfiles en dependencias
    /// </summary>
    public virtual ICollection<UsuarioPerfilDependencia> PerfilesDependencias { get; set; } = new List<UsuarioPerfilDependencia>();
}