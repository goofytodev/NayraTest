//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Entities
//{
//    internal class Persona
//    {
//    }
//}
using NayraBase.Domain.Entities.Common;
using NayraBase.Domain.Enums;

namespace NayraBase.Domain.Entities;

/// <summary>
/// Entidad que representa los datos personales de un individuo
/// </summary>
public class Persona : BaseEntity
{
    /// <summary>
    /// Nombres de la persona
    /// </summary>
    public string Nombres { get; set; } = string.Empty;

    /// <summary>
    /// Apellido paterno
    /// </summary>
    public string ApellidoPaterno { get; set; } = string.Empty;

    /// <summary>
    /// Apellido materno
    /// </summary>
    public string? ApellidoMaterno { get; set; }

    /// <summary>
    /// Tipo de documento de identidad
    /// </summary>
    public TipoDocumento TipoDocumento { get; set; }

    /// <summary>
    /// Número de documento (único)
    /// </summary>
    public string NumeroDocumento { get; set; } = string.Empty;

    /// <summary>
    /// Fecha de nacimiento
    /// </summary>
    public DateTime? FechaNacimiento { get; set; }

    /// <summary>
    /// Género
    /// </summary>
    public Genero? Genero { get; set; }

    /// <summary>
    /// Número de teléfono
    /// </summary>
    public string? Telefono { get; set; }

    /// <summary>
    /// Correo electrónico
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Dirección de residencia
    /// </summary>
    public string? Direccion { get; set; }

    /// <summary>
    /// Indica si la persona está activa en el sistema
    /// </summary>
    public bool Estado { get; set; } = true;

    // Navegación

    /// <summary>
    /// Usuario asociado a esta persona (puede ser null)
    /// </summary>
    public virtual Usuario? Usuario { get; set; }
}