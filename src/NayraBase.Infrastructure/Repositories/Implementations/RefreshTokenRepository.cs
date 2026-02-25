using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Repositories.Interfaces;

namespace NayraBase.Infrastructure.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de RefreshToken
/// </summary>
public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(NayraBaseDbContext context) : base(context)
    {
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _dbSet
            .Include(rt => rt.Usuario)
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task<IEnumerable<RefreshToken>> GetByUsuarioIdAsync(int usuarioId)
    {
        return await _dbSet
            .Where(rt => rt.UsuarioId == usuarioId)
            .OrderByDescending(rt => rt.FechaCreacion)
            .ToListAsync();
    }

    public async Task RevokeTokenAsync(string token)
    {
        var refreshToken = await _dbSet.FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken != null)
        {
            refreshToken.Revocado = true;
            refreshToken.FechaRevocacion = DateTime.Now;
            refreshToken.FechaActualizacion = DateTime.Now;
        }
    }

    public async Task RevokeAllUserTokensAsync(int usuarioId)
    {
        var tokens = await _dbSet
            .Where(rt => rt.UsuarioId == usuarioId && !rt.Revocado)
            .ToListAsync();

        foreach (var token in tokens)
        {
            token.Revocado = true;
            token.FechaRevocacion = DateTime.Now;
            token.FechaActualizacion = DateTime.Now;
        }
    }

    public async Task<int> DeleteExpiredTokensAsync()
    {
        var expiredTokens = await _dbSet
            .Where(rt => rt.FechaExpiracion < DateTime.Now)
            .ToListAsync();

        _dbSet.RemoveRange(expiredTokens);

        return expiredTokens.Count;
    }
}