namespace NayraBase.Infrastructure.Security;

/// <summary>
/// Normalizador de texto para username, email, etc.
/// </summary>
public static class TextNormalizer
{
    /// <summary>
    /// Normaliza un texto a mayúsculas (para búsquedas case-insensitive)
    /// </summary>
    /// <param name="text">Texto a normalizar</param>
    /// <returns>Texto normalizado en mayúsculas</returns>
    public static string Normalize(string? text)
    {
        return text?.ToUpperInvariant() ?? string.Empty;
    }

    /// <summary>
    /// Normaliza un username
    /// </summary>
    public static string NormalizeUsername(string username)
    {
        return Normalize(username.Trim());
    }

    /// <summary>
    /// Normaliza un email
    /// </summary>
    public static string NormalizeEmail(string email)
    {
        return Normalize(email.Trim());
    }
}