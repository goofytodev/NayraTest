using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Repositories.Interfaces;
using NayraBase.Infrastructure.Security;

namespace NayraBase.Infrastructure.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de Usuario
/// </summary>
public class UsuarioRepository : GenericRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(NayraBaseDbContext context) : base(context)
    {
    }

    #region Búsquedas específicas

    public async Task<Usuario?> GetByUsernameAsync(string username)
    {
        var normalizedUsername = TextNormalizer.NormalizeUsername(username);
        return await _dbSet
            .FirstOrDefaultAsync(u => u.NormalizedUsername == normalizedUsername);
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        var normalizedEmail = TextNormalizer.NormalizeEmail(email);
        return await _dbSet
            .FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
    }

    public async Task<Usuario?> GetByNumeroDocumentoAsync(string numeroDocumento)
    {
        return await _dbSet
            .Include(u => u.Persona)
            .FirstOrDefaultAsync(u => u.Persona.NumeroDocumento == numeroDocumento);
    }

    public async Task<Usuario?> GetByIdWithPersonaAsync(int id)
    {
        return await _dbSet
            .Include(u => u.Persona)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<Usuario?> GetByUsernameWithPersonaAsync(string username)
    {
        var normalizedUsername = TextNormalizer.NormalizeUsername(username);
        return await _dbSet
            .Include(u => u.Persona)
            .FirstOrDefaultAsync(u => u.NormalizedUsername == normalizedUsername);
    }

    #endregion

    #region Validaciones

    public async Task<bool> ExistsUsernameAsync(string username, int? excludeUserId = null)
    {
        var normalizedUsername = TextNormalizer.NormalizeUsername(username);

        var query = _dbSet.Where(u => u.NormalizedUsername == normalizedUsername);

        if (excludeUserId.HasValue)
        {
            query = query.Where(u => u.Id != excludeUserId.Value);
        }

        return await query.AnyAsync();
    }

    public async Task<bool> ExistsEmailAsync(string email, int? excludeUserId = null)
    {
        var normalizedEmail = TextNormalizer.NormalizeEmail(email);

        var query = _dbSet.Where(u => u.NormalizedEmail == normalizedEmail);

        if (excludeUserId.HasValue)
        {
            query = query.Where(u => u.Id != excludeUserId.Value);
        }

        return await query.AnyAsync();
    }

    #endregion

    #region Lockout y seguridad

    public async Task IncrementAccessFailedCountAsync(int userId)
    {
        var usuario = await _dbSet.FindAsync(userId);
        if (usuario == null) return;

        usuario.AccessFailedCount++;

        // Bloquear si alcanza el límite
        if (usuario.AccessFailedCount >= 5)
        {
            usuario.LockoutEnd = DateTime.Now.AddMinutes(15);
        }

        usuario.FechaActualizacion = DateTime.Now;
    }

    public async Task ResetAccessFailedCountAsync(int userId)
    {
        var usuario = await _dbSet.FindAsync(userId);
        if (usuario == null) return;

        usuario.AccessFailedCount = 0;
        usuario.LockoutEnd = null;
        usuario.FechaActualizacion = DateTime.Now;
    }

    public async Task SetLockoutEndDateAsync(int userId, DateTime? lockoutEnd)
    {
        var usuario = await _dbSet.FindAsync(userId);
        if (usuario == null) return;

        usuario.LockoutEnd = lockoutEnd;
        usuario.FechaActualizacion = DateTime.Now;
    }

    public async Task UpdateSecurityStampAsync(int userId)
    {
        var usuario = await _dbSet.FindAsync(userId);
        if (usuario == null) return;

        usuario.SecurityStamp = SecurityStampGenerator.Generate();
        usuario.FechaActualizacion = DateTime.Now;
    }

    public async Task UpdateLastAccessAsync(int userId)
    {
        var usuario = await _dbSet.FindAsync(userId);
        if (usuario == null) return;

        usuario.UltimoAcceso = DateTime.Now;
        usuario.FechaActualizacion = DateTime.Now;
    }

    #endregion

    #region Consultas avanzadas

    public async Task<IEnumerable<Usuario>> GetUsuariosPorDependenciaAsync(int dependenciaId)
    {
        return await _dbSet
            .Include(u => u.Persona)
            .Include(u => u.PerfilesDependencias)
                .ThenInclude(upd => upd.Perfil)
            .Where(u => u.PerfilesDependencias.Any(upd => upd.DependenciaId == dependenciaId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosActivosAsync()
    {
        return await _dbSet
            .Include(u => u.Persona)
            .Where(u => u.Activo)
            .ToListAsync();
    }

    public async Task<IEnumerable<Usuario>> GetUsuariosBloqueadosAsync()
    {
        return await _dbSet
            .Include(u => u.Persona)
            .Where(u => u.LockoutEnd.HasValue && u.LockoutEnd.Value > DateTime.Now)
            .ToListAsync();
    }

    #endregion
}