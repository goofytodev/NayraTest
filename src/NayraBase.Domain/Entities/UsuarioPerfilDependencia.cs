//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class UsuarioPerfilDependencia
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad CENTRAL que representa la asignación de un perfil a un usuario en una dependencia
/// Implementa el modelo RBAC jerárquico
/// </summary>
public class UsuarioPerfilDependencia : BaseEntity
{
    /// <summary>
    /// ID del usuario
    /// </summary>
    public int UsuarioId { get; set; }

    /// <summary>
    /// ID del perfil asignado
    /// </summary>
    public int PerfilId { get; set; }

    /// <summary>
    /// ID de la dependencia donde tiene este perfil
    /// </summary>
    public int DependenciaId { get; set; }

    // Control de herencia

    /// <summary>
    /// Indica si los permisos se heredan a subdependencias
    /// </summary>
    public bool HeredaASubdependencias { get; set; } = false;

    /// <summary>
    /// ID del modo de herencia (define cómo se heredan los permisos)
    /// </summary>
    public int? ModoHerenciaId { get; set; }

    // Restricciones adicionales

    /// <summary>
    /// Indica si solo tiene permisos de lectura (sobrescribe permisos del perfil)
    /// </summary>
    public bool SoloLectura { get; set; } = false;

    /// <summary>
    /// IDs de permisos adicionales (fuera del perfil)
    /// </summary>
    public int[]? PermisosAdicionales { get; set; }

    /// <summary>
    /// IDs de permisos revocados (del perfil)
    /// </summary>
    public int[]? PermisosRevocados { get; set; }

    // Auditoría

    /// <summary>
    /// Fecha de asignación
    /// </summary>
    public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Fecha de fin de la asignación (NULL = vigente)
    /// </summary>
    public DateTime? FechaFin { get; set; }

    /// <summary>
    /// Usuario que realizó la asignación
    /// </summary>
    public int? AsignadoPor { get; set; }

    /// <summary>
    /// Motivo de la asignación
    /// </summary>
    public string? MotivoAsignacion { get; set; }

    /// <summary>
    /// Documento de sustento (Resolución, Memorándum, etc.)
    /// </summary>
    public string? DocumentoSustento { get; set; }

    // Navegación

    /// <summary>
    /// Usuario asignado
    /// </summary>
    public virtual Usuario Usuario { get; set; } = null!;

    /// <summary>
    /// Perfil asignado
    /// </summary>
    public virtual Perfil Perfil { get; set; } = null!;

    /// <summary>
    /// Dependencia donde está asignado
    /// </summary>
    public virtual Dependencia Dependencia { get; set; } = null!;

    /// <summary>
    /// Modo de herencia aplicado
    /// </summary>
    public virtual ModoHerencia? ModoHerencia { get; set; }
}