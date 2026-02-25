using Microsoft.EntityFrameworkCore.Storage;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Repositories.Implementations;
using NayraBase.Infrastructure.Repositories.Interfaces;

namespace NayraBase.Infrastructure.UnitOfWork;

/// <summary>
/// Implementación del patrón Unit of Work
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly NayraBaseDbContext _context;
    private IDbContextTransaction? _transaction;

    // Repositorios (lazy initialization)
    private IUsuarioRepository? _usuarios;
    private IPersonaRepository? _personas;
    private IDependenciaRepository? _dependencias;
    private IPerfilRepository? _perfiles;
    private IPermisoRepository? _permisos;
    private IModoHerenciaRepository? _modosHerencia;
    private IRefreshTokenRepository? _refreshTokens;

    public UnitOfWork(NayraBaseDbContext context)
    {
        _context = context;
    }

    #region Repositorios

    public IUsuarioRepository Usuarios
    {
        get
        {
            _usuarios ??= new UsuarioRepository(_context);
            return _usuarios;
        }
    }

    public IPersonaRepository Personas
    {
        get
        {
            _personas ??= new PersonaRepository(_context);
            return _personas;
        }
    }

    public IDependenciaRepository Dependencias
    {
        get
        {
            _dependencias ??= new DependenciaRepository(_context);
            return _dependencias;
        }
    }

    public IPerfilRepository Perfiles
    {
        get
        {
            _perfiles ??= new PerfilRepository(_context);
            return _perfiles;
        }
    }

    public IPermisoRepository Permisos
    {
        get
        {
            _permisos ??= new PermisoRepository(_context);
            return _permisos;
        }
    }

    public IModoHerenciaRepository ModosHerencia
    {
        get
        {
            _modosHerencia ??= new ModoHerenciaRepository(_context);
            return _modosHerencia;
        }
    }

    public IRefreshTokenRepository RefreshTokens
    {
        get
        {
            _refreshTokens ??= new RefreshTokenRepository(_context);
            return _refreshTokens;
        }
    }

    #endregion

    #region Transacciones

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();

            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    #endregion

    #region Dispose

    public void Dispose()
    {
        _transaction?.Dispose();
        _context.Dispose();
        GC.SuppressFinalize(this);
    }

    #endregion
}