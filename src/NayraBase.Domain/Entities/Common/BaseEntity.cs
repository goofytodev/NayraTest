//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities.Common
//{
//    internal class BaseEntity
//    {
//    }
//}
namespace NayraBase.Domain.Entities.Common;

/// <summary>
/// Clase base para todas las entidades
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Identificador único de la entidad
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Fecha de creación del registro
    /// </summary>
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de última actualización del registro
    /// </summary>
    public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
}