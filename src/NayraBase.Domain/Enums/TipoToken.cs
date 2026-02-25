//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Domain.Enums
//{
//    internal class TipoToken
//    {
//    }
//}
namespace NayraBase.Domain.Enums;

/// <summary>
/// Tipos de token de confirmación
/// </summary>
public enum TipoToken
{
    /// <summary>
    /// Token para confirmar email
    /// </summary>
    ConfirmacionEmail = 1,

    /// <summary>
    /// Token para resetear contraseña
    /// </summary>
    ResetPassword = 2,

    /// <summary>
    /// Token para cambiar email
    /// </summary>
    CambioEmail = 3,

    /// <summary>
    /// Token para verificar teléfono
    /// </summary>
    VerificacionTelefono = 4
}