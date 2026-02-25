using NayraBase.Domain.Entities;
using System.Numerics;

namespace NayraBase.Infrastructure.Repositories.Interfaces;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<IEnumerable<RefreshToken>> GetByUsuarioIdAsync(int usuarioId);
    Task RevokeTokenAsync(string token);
    Task RevokeAllUserTokensAsync(int usuarioId);
    Task<int> DeleteExpiredTokensAsync();
}