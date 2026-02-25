using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Repositorio específico para Usuario con métodos adicionales
/// </summary>
public interface IUsuarioRepository : IGenericRepository<Usuario>
{
    // Búsquedas específicas
    Task<Usuario?> GetByUsernameAsync(string username);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<Usuario?> GetByNumeroDocumentoAsync(string numeroDocumento);
    Task<Usuario?> GetByIdWithPersonaAsync(int id);
    Task<Usuario?> GetByUsernameWithPersonaAsync(string username);

    // Validaciones
    Task<bool> ExistsUsernameAsync(string username, int? excludeUserId = null);
    Task<bool> ExistsEmailAsync(string email, int? excludeUserId = null);

    // Lockout y seguridad
    Task IncrementAccessFailedCountAsync(int userId);
    Task ResetAccessFailedCountAsync(int userId);
    Task SetLockoutEndDateAsync(int userId, DateTime? lockoutEnd);
    Task UpdateSecurityStampAsync(int userId);
    Task UpdateLastAccessAsync(int userId);

    // Consultas avanzadas
    Task<IEnumerable<Usuario>> GetUsuariosPorDependenciaAsync(int dependenciaId);
    Task<IEnumerable<Usuario>> GetUsuariosActivosAsync();
    Task<IEnumerable<Usuario>> GetUsuariosBloqueadosAsync();
}