//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class PerfilPermiso
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa la relación muchos a muchos entre Perfil y Permiso
/// Define qué permisos tiene cada perfil
/// </summary>
public class PerfilPermiso : BaseEntity
{
    /// <summary>
    /// ID del perfil
    /// </summary>
    public int PerfilId { get; set; }

    /// <summary>
    /// ID del permiso
    /// </summary>
    public int PermisoId { get; set; }

    /// <summary>
    /// Indica si este permiso puede ser delegado por usuarios con este perfil
    /// </summary>
    public bool PuedeDelegar { get; set; } = false;

    // Navegación

    /// <summary>
    /// Perfil asociado
    /// </summary>
    public virtual Perfil Perfil { get; set; } = null!;

    /// <summary>
    /// Permiso asociado
    /// </summary>
    public virtual Permiso Permiso { get; set; } = null!;
}