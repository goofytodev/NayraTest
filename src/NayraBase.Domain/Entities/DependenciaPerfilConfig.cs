//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class DependenciaPerfilConfig
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que configura qué perfiles están disponibles en cada dependencia
/// </summary>
public class DependenciaPerfilConfig : BaseEntity
{
    /// <summary>
    /// ID de la dependencia
    /// </summary>
    public int DependenciaId { get; set; }

    /// <summary>
    /// ID del perfil
    /// </summary>
    public int PerfilId { get; set; }

    /// <summary>
    /// Indica si la configuración está activa
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Cantidad máxima de usuarios con este perfil en esta dependencia
    /// NULL = sin límite
    /// </summary>
    public int? CantidadMaxima { get; set; }

    /// <summary>
    /// Indica si requiere aprobación para asignar este perfil
    /// </summary>
    public bool RequiereAprobacion { get; set; } = false;

    /// <summary>
    /// Usuario que configuró este perfil en la dependencia
    /// </summary>
    public int? ConfiguradoPor { get; set; }

    /// <summary>
    /// Fecha de configuración
    /// </summary>
    public DateTime FechaConfiguracion { get; set; } = DateTime.UtcNow;

    // Navegación

    /// <summary>
    /// Dependencia asociada
    /// </summary>
    public virtual Dependencia Dependencia { get; set; } = null!;

    /// <summary>
    /// Perfil asociado
    /// </summary>
    public virtual Perfil Perfil { get; set; } = null!;
}