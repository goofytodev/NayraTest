//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class Permiso
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa un permiso granular en el sistema
/// </summary>
public class Permiso : BaseEntity
{
    /// <summary>
    /// Código único del permiso
    /// Ejemplo: "ASISTENCIA_CREAR_PROPIO"
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    /// Módulo al que pertenece el permiso
    /// Ejemplo: "RRHH", "Finanzas", "Logística"
    /// </summary>
    public string Modulo { get; set; } = string.Empty;

    /// <summary>
    /// Recurso sobre el que actúa
    /// Ejemplo: "Asistencia", "Documento", "Usuario"
    /// </summary>
    public string Recurso { get; set; } = string.Empty;

    /// <summary>
    /// Acción que se puede realizar
    /// Ejemplo: "Crear", "Leer", "Actualizar", "Eliminar", "Aprobar"
    /// </summary>
    public string Accion { get; set; } = string.Empty;

    /// <summary>
    /// Alcance del permiso
    /// Ejemplo: "Propio", "Dependencia", "Subdependencias", "Todo"
    /// </summary>
    public string Alcance { get; set; } = string.Empty;

    /// <summary>
    /// Indica si es un permiso operativo (modifica datos)
    /// </summary>
    public bool EsOperativo { get; set; } = true;

    /// <summary>
    /// Indica si es un permiso de supervisión (solo lectura supervisora)
    /// </summary>
    public bool EsSupervision { get; set; } = false;

    /// <summary>
    /// Indica si es un permiso de auditoría
    /// </summary>
    public bool EsAuditoria { get; set; } = false;

    /// <summary>
    /// Descripción del permiso
    /// </summary>
    public string? Descripcion { get; set; }

    /// <summary>
    /// Indica si requiere justificación al usarse
    /// </summary>
    public bool RequiereJustificacion { get; set; } = false;

    /// <summary>
    /// Nivel de sensibilidad (1=bajo, 2=medio, 3=alto, 4=crítico)
    /// </summary>
    public int NivelSensibilidad { get; set; } = 1;

    /// <summary>
    /// Indica si el permiso está activo
    /// </summary>
    public bool Activo { get; set; } = true;

    /// <summary>
    /// Indica si es un permiso del sistema (no se puede eliminar)
    /// </summary>
    public bool EsSistema { get; set; } = false;

    // Navegación

    /// <summary>
    /// Perfiles que tienen este permiso asignado
    /// </summary>
    public virtual ICollection<PerfilPermiso> PerfilPermisos { get; set; } = new List<PerfilPermiso>();
}