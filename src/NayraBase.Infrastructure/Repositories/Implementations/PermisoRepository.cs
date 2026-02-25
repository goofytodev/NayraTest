using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Repositories.Interfaces;

namespace NayraBase.Infrastructure.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de Permiso
/// </summary>
public class PermisoRepository : GenericRepository<Permiso>, IPermisoRepository
{
    public PermisoRepository(NayraBaseDbContext context) : base(context)
    {
    }

    public async Task<Permiso?> GetByCodigoAsync(string codigo)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.Codigo == codigo);
    }

    public async Task<IEnumerable<Permiso>> GetByModuloAsync(string modulo)
    {
        return await _dbSet
            .Where(p => p.Modulo == modulo && p.Activo)
            .OrderBy(p => p.Recurso)
            .ThenBy(p => p.Accion)
            .ToListAsync();
    }

    public async Task<IEnumerable<Permiso>> GetPermisosOperativosAsync()
    {
        return await _dbSet
            .Where(p => p.EsOperativo && p.Activo)
            .OrderBy(p => p.Modulo)
            .ThenBy(p => p.Recurso)
            .ToListAsync();
    }

    public async Task<IEnumerable<Permiso>> GetPermisosSupervisionAsync()
    {
        return await _dbSet
            .Where(p => p.EsSupervision && p.Activo)
            .OrderBy(p => p.Modulo)
            .ThenBy(p => p.Recurso)
            .ToListAsync();
    }
}