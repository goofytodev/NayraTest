//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class ModoHerencia
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa un modo de herencia de permisos
/// Define cómo se comportan los permisos cuando se heredan en la jerarquía
/// </summary>
public class ModoHerencia : BaseEntity
{
    /// <summary>
    /// Código único del modo
    /// Ejemplo: "SUPERVISION", "OPERATIVO", "AUDITORIA"
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del modo
    /// Ejemplo: "Supervisión", "Operativo Heredado", "Auditoría"
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Descripción del modo
    /// </summary>
    public string? Descripcion { get; set; }

    // Comportamiento del modo

    /// <summary>
    /// Indica si permite realizar operaciones (modificar datos)
    /// </summary>
    public bool PermiteOperaciones { get; set; } = false;

    /// <summary>
    /// Indica si permite lectura de datos
    /// </summary>
    public bool PermiteLectura { get; set; } = true;

    /// <summary>
    /// Indica si permite generar reportes
    /// </summary>
    public bool PermiteReportes { get; set; } = true;

    /// <summary>
    /// Indica si permite aprobaciones
    /// </summary>
    public bool PermiteAprobaciones { get; set; } = false;

    /// <summary>
    /// Configuración de filtros de permisos en formato JSON
    /// Define qué tipos de permisos incluir/excluir
    /// </summary>
    public string? FiltroPermisos { get; set; }

    // Configuración visual

    /// <summary>
    /// Color hexadecimal para UI
    /// Ejemplo: "#3B82F6"
    /// </summary>
    public string? ColorHex { get; set; }

    /// <summary>
    /// Icono para UI
    /// Ejemplo: "eye", "edit", "shield"
    /// </summary>
    public string? Icono { get; set; }

    /// <summary>
    /// Nivel de acceso (1=bajo, 5=alto)
    /// </summary>
    public int NivelAcceso { get; set; } = 1;

    /// <summary>
    /// Indica si es un modo del sistema (no se puede eliminar)
    /// </summary>
    public bool EsSistema { get; set; } = false;

    /// <summary>
    /// Indica si el modo está activo
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Usuario que creó el modo
    /// </summary>
    public int? CreadoPor { get; set; }

    // Navegación

    /// <summary>
    /// Asignaciones que usan este modo de herencia
    /// </summary>
    public virtual ICollection<UsuarioPerfilDependencia> AsignacionesConModo { get; set; } = new List<UsuarioPerfilDependencia>();
}