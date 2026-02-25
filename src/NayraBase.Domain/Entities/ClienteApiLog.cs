//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class ClienteApiLog
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que registra los accesos y uso de clientes API
/// Auditoría completa de requests
/// </summary>
public class ClienteApiLog : BaseEntity
{
    /// <summary>
    /// ID del cliente que realizó el request
    /// </summary>
    public int ClienteApiId { get; set; }

    /// <summary>
    /// Endpoint/ruta accedida
    /// Ejemplo: "/api/usuarios", "/api/dependencias/5"
    /// </summary>
    public string Endpoint { get; set; } = string.Empty;

    /// <summary>
    /// Método HTTP utilizado
    /// Ejemplo: "GET", "POST", "PUT", "DELETE"
    /// </summary>
    public string Metodo { get; set; } = string.Empty;

    /// <summary>
    /// Dirección IP del cliente
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// User Agent del cliente
    /// </summary>
    public string? UserAgent { get; set; }

    /// <summary>
    /// Fecha y hora del request
    /// </summary>
    public DateTime FechaRequest { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Código de respuesta HTTP
    /// Ejemplo: 200, 401, 403, 404, 500
    /// </summary>
    public int ResponseCode { get; set; }

    /// <summary>
    /// Tiempo de procesamiento en milisegundos (opcional)
    /// </summary>
    public int? DuracionMs { get; set; }

    /// <summary>
    /// Mensaje de error (si hubo error)
    /// </summary>
    public string? ErrorMessage { get; set; }

    // Navegación

    /// <summary>
    /// Cliente que realizó el request
    /// </summary>
    public virtual ClienteApi ClienteApi { get; set; } = null!;
}