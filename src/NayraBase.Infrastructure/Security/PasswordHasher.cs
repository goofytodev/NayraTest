using Microsoft.AspNetCore.Identity;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Security;

/// <summary>
/// Servicio para hashear y verificar contraseñas usando PBKDF2
/// Wrapper de Microsoft.AspNetCore.Identity.PasswordHasher
/// </summary>
public class PasswordHasher
{
    private readonly PasswordHasher<Usuario> _passwordHasher;

    public PasswordHasher()
    {
        _passwordHasher = new PasswordHasher<Usuario>();
    }

    /// <summary>
    /// Hashea una contraseña en texto plano
    /// </summary>
    /// <param name="password">Contraseña en texto plano</param>
    /// <returns>Hash de la contraseña</returns>
    public string HashPassword(string password)
    {
        // Crear un usuario temporal solo para el hash
        var tempUser = new Usuario();
        return _passwordHasher.HashPassword(tempUser, password);
    }

    /// <summary>
    /// Verifica si una contraseña coincide con su hash
    /// </summary>
    /// <param name="hashedPassword">Hash almacenado en la BD</param>
    /// <param name="providedPassword">Contraseña proporcionada por el usuario</param>
    /// <returns>True si la contraseña es correcta</returns>
    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        var tempUser = new Usuario();
        var result = _passwordHasher.VerifyHashedPassword(
            tempUser,
            hashedPassword,
            providedPassword
        );

        return result == PasswordVerificationResult.Success ||
               result == PasswordVerificationResult.SuccessRehashNeeded;
    }

    /// <summary>
    /// Verifica si el hash necesita ser regenerado
    /// (por ejemplo, si cambió el número de iteraciones)
    /// </summary>
    public bool NeedsRehash(string hashedPassword, string providedPassword)
    {
        var tempUser = new Usuario();
        var result = _passwordHasher.VerifyHashedPassword(
            tempUser,
            hashedPassword,
            providedPassword
        );

        return result == PasswordVerificationResult.SuccessRehashNeeded;
    }
}