//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class TokenConfirmacion
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;
using NayraBase.Domain.Enums;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa tokens de confirmación
/// (confirmación de email, reset de password, etc.)
/// </summary>
public class TokenConfirmacion : BaseEntity
{
    /// <summary>
    /// ID del usuario asociado
    /// </summary>
    public int UsuarioId { get; set; }

    /// <summary>
    /// Token único (GUID)
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Tipo de token (ConfirmacionEmail, ResetPassword, etc.)
    /// </summary>
    public TipoToken Tipo { get; set; }

    /// <summary>
    /// Fecha y hora de expiración del token
    /// </summary>
    public DateTime FechaExpiracion { get; set; }

    /// <summary>
    /// Indica si el token ya fue usado
    /// </summary>
    public bool Usado { get; set; } = false;

    /// <summary>
    /// Fecha y hora en que fue usado
    /// </summary>
    public DateTime? FechaUso { get; set; }

    /// <summary>
    /// Dirección IP desde donde se usó el token
    /// </summary>
    public string? IpAddressUso { get; set; }

    // Propiedades calculadas

    /// <summary>
    /// Indica si el token es válido (no usado y no expirado)
    /// </summary>
    public bool EsValido => !Usado && DateTime.UtcNow < FechaExpiracion;

    /// <summary>
    /// Indica si el token ha expirado
    /// </summary>
    public bool EstaExpirado => DateTime.UtcNow >= FechaExpiracion;

    // Navegación

    /// <summary>
    /// Usuario asociado al token
    /// </summary>
    public virtual Usuario Usuario { get; set; } = null!;
}