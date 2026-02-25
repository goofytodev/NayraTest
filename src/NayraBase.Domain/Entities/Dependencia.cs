//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class Dependencia
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa una dependencia organizacional
/// Implementa estructura de árbol jerárquico con auto-referencia
/// </summary>
public class Dependencia : BaseEntity
{
    /// <summary>
    /// Nombre de la dependencia
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    /// Código único de la dependencia (opcional)
    /// </summary>
    public string? Codigo { get; set; }

    /// <summary>
    /// Descripción de la dependencia
    /// </summary>
    public string? Descripcion { get; set; }

    // Auto-referencia (Árbol Jerárquico)

    /// <summary>
    /// ID de la dependencia padre (NULL = es raíz)
    /// </summary>
    public int? DependenciaPadreId { get; set; }

    /// <summary>
    /// Nivel de profundidad en el árbol (0 = raíz)
    /// </summary>
    public int Nivel { get; set; } = 0;

    /// <summary>
    /// Ruta completa desde la raíz
    /// Ejemplo: "Organización > Gerencia > IT > Desarrollo"
    /// </summary>
    public string? RutaCompleta { get; set; }

    /// <summary>
    /// Indica si es un nodo hoja (sin hijos)
    /// </summary>
    public bool EsHoja { get; set; } = true;

    /// <summary>
    /// Orden de presentación entre hermanos
    /// </summary>
    public int Orden { get; set; } = 0;

    /// <summary>
    /// Indica si la dependencia está activa
    /// </summary>
    public bool Estado { get; set; } = true;

    // Navegación

    /// <summary>
    /// Dependencia padre (null si es raíz)
    /// </summary>
    public virtual Dependencia? DependenciaPadre { get; set; }

    /// <summary>
    /// Dependencias hijas
    /// </summary>
    public virtual ICollection<Dependencia> DependenciasHijas { get; set; } = new List<Dependencia>();

    /// <summary>
    /// Configuraciones de perfiles disponibles en esta dependencia
    /// </summary>
    public virtual ICollection<DependenciaPerfilConfig> ConfiguracionPerfiles { get; set; } = new List<DependenciaPerfilConfig>();

    /// <summary>
    /// Asignaciones de usuarios con perfiles en esta dependencia
    /// </summary>
    public virtual ICollection<UsuarioPerfilDependencia> UsuariosAsignados { get; set; } = new List<UsuarioPerfilDependencia>();
}