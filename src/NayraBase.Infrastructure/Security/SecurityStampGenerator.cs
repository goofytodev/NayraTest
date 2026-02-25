namespace NayraBase.Infrastructure.Security;

/// <summary>
/// Generador de Security Stamps (GUIDs únicos)
/// </summary>
public static class SecurityStampGenerator
{
    /// <summary>
    /// Genera un nuevo security stamp
    /// </summary>
    /// <returns>GUID como string</returns>
    public static string Generate()
    {
        return Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Genera un nuevo concurrency stamp
    /// </summary>
    /// <returns>GUID como string</returns>
    public static string GenerateConcurrencyStamp()
    {
        return Guid.NewGuid().ToString();
    }
}