//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class Perfil
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa un perfil/rol dentro del sistema
/// Agrupa un conjunto de permisos
/// </summary>
public class Perfil : BaseEntity
{
    /// <summary>
    /// Nombre del perfil (único)
    /// Ejemplo: "Director", "Jefe de Área", "Técnico Operativo"
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del perfil
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Nivel jerárquico del perfil (1=más alto, 5=más bajo)
    /// 1: Director, 2: Jefe, 3: Supervisor, 4: Analista, 5: Técnico
    /// </summary>
    public int NivelJerarquico { get; set; } = 5;

    /// <summary>
    /// Indica si es un perfil operativo (modifica datos)
    /// </summary>
    public bool EsOperativo { get; set; } = true;

    /// <summary>
    /// Indica si es un perfil de supervisión (solo lectura supervisora)
    /// </summary>
    public bool EsSupervision { get; set; } = false;

    /// <summary>
    /// Indica si el perfil está activo
    /// </summary>
    public bool Activo { get; set; } = true;

    // Navegación

    /// <summary>
    /// Permisos asignados a este perfil
    /// </summary>
    public virtual ICollection<PerfilPermiso> PerfilPermisos { get; set; } = new List<PerfilPermiso>();

    /// <summary>
    /// Configuraciones de este perfil en diferentes dependencias
    /// </summary>
    public virtual ICollection<DependenciaPerfilConfig> ConfiguracionesDependencias { get; set; } = new List<DependenciaPerfilConfig>();

    /// <summary>
    /// Asignaciones de este perfil a usuarios
    /// </summary>
    public virtual ICollection<UsuarioPerfilDependencia> AsignacionesUsuarios { get; set; } = new List<UsuarioPerfilDependencia>();
}