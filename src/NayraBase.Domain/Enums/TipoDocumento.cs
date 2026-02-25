//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Enums
//{
//    internal class TipoDocumento
//    {
//    }
//}
namespace NayraBase.Domain.Enums;

/// <summary>
/// Tipos de documento de identidad
/// </summary>
public enum TipoDocumento
{
    /// <summary>
    /// Documento Nacional de Identidad
    /// </summary>
    DNI = 1,

    /// <summary>
    /// Carnet de Extranjería
    /// </summary>
    CarnetExtranjeria = 2,

    /// <summary>
    /// Pasaporte
    /// </summary>
    Pasaporte = 3,

    /// <summary>
    /// Registro Único de Contribuyentes
    /// </summary>
    RUC = 4,

    /// <summary>
    /// Partida de Nacimiento
    /// </summary>
    PartidaNacimiento = 5
}