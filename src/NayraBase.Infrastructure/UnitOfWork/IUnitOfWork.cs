using NayraBase.Infrastructure.Repositories.Interfaces;

namespace NayraBase.Infrastructure.UnitOfWork;

/// <summary>
/// Patrón Unit of Work para coordinar transacciones
/// </summary>
public interface IUnitOfWork : IDisposable
{
    // Repositorios
    IUsuarioRepository Usuarios { get; }
    IPersonaRepository Personas { get; }
    IDependenciaRepository Dependencias { get; }
    IPerfilRepository Perfiles { get; }
    IPermisoRepository Permisos { get; }
    IModoHerenciaRepository ModosHerencia { get; }
    IRefreshTokenRepository RefreshTokens { get; }

    // Transacciones
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}