//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class RefreshToken
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa un token de actualización (refresh token)
/// Permite renovar el JWT sin solicitar credenciales nuevamente
/// </summary>
public class RefreshToken : BaseEntity
{
    /// <summary>
    /// ID del usuario propietario del token
    /// </summary>
    public int UsuarioId { get; set; }

    /// <summary>
    /// Token único (GUID)
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Fecha y hora de expiración del token
    /// </summary>
    public DateTime FechaExpiracion { get; set; }

    /// <summary>
    /// Indica si el token ha sido revocado
    /// </summary>
    public bool Revocado { get; set; } = false;

    /// <summary>
    /// Fecha y hora en que fue revocado
    /// </summary>
    public DateTime? FechaRevocacion { get; set; }

    /// <summary>
    /// Token que reemplazó a este (cuando se rota)
    /// </summary>
    public string? ReemplazadoPor { get; set; }

    /// <summary>
    /// Dirección IP desde donde se creó el token
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// User Agent del cliente que solicitó el token
    /// </summary>
    public string? UserAgent { get; set; }

    // Propiedades calculadas

    /// <summary>
    /// Indica si el token está activo (no revocado y no expirado)
    /// </summary>
    public bool EstaActivo => !Revocado && DateTime.UtcNow < FechaExpiracion;

    /// <summary>
    /// Indica si el token ha expirado
    /// </summary>
    public bool EstaExpirado => DateTime.UtcNow >= FechaExpiracion;

    // Navegación

    /// <summary>
    /// Usuario propietario del token
    /// </summary>
    public virtual Usuario Usuario { get; set; } = null!;
}