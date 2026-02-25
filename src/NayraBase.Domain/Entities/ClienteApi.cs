//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class ClienteApi
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa una aplicación cliente autorizada para acceder a la API
/// Implementa autenticación de cliente (API Key)
/// </summary>
public class ClienteApi : BaseEntity
{
    /// <summary>
    /// Nombre de la aplicación cliente
    /// Ejemplo: "Angular Web App", "Mobile App Android", "Sistema Externo X"
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del cliente
    /// </summary>
    public string? Descripcion { get; set; }

    // Credenciales

    /// <summary>
    /// Identificador público del cliente (API Key)
    /// Ejemplo: "client_abc123xyz456"
    /// </summary>
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Hash del secreto del cliente (API Secret)
    /// Se hashea como una contraseña
    /// </summary>
    public string ClientSecretHash { get; set; } = string.Empty;

    // Configuración de seguridad

    /// <summary>
    /// Orígenes permitidos (CORS)
    /// Ejemplo: ["https://app.miempresa.com", "https://admin.miempresa.com"]
    /// </summary>
    public string[]? AllowedOrigins { get; set; }

    /// <summary>
    /// Direcciones IP permitidas (opcional)
    /// NULL o vacío = todas las IPs permitidas
    /// </summary>
    public string[]? AllowedIps { get; set; }

    /// <summary>
    /// Límite de requests por hora
    /// </summary>
    public int RateLimit { get; set; } = 1000;

    /// <summary>
    /// Scopes/permisos del cliente
    /// Ejemplo: ["read:users", "write:users", "read:dependencias"]
    /// </summary>
    public string[]? Scopes { get; set; }

    // Control de estado

    /// <summary>
    /// Indica si el cliente está activo
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Fecha de expiración del cliente (NULL = sin expiración)
    /// </summary>
    public DateTime? FechaExpiracion { get; set; }

    // Auditoría

    /// <summary>
    /// Usuario que creó/registró el cliente
    /// </summary>
    public int? CreadoPor { get; set; }

    /// <summary>
    /// Fecha del último uso del cliente
    /// </summary>
    public DateTime? UltimoUso { get; set; }

    // Navegación

    /// <summary>
    /// Logs de uso de este cliente
    /// </summary>
    public virtual ICollection<ClienteApiLog> Logs { get; set; } = new List<ClienteApiLog>();

    /// <summary>
    /// Usuario que creó el cliente
    /// </summary>
    public virtual Usuario? Creador { get; set; }
}