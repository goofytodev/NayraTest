using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Repositories.Interfaces;

namespace NayraBase.Infrastructure.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de Perfil
/// </summary>
public class PerfilRepository : GenericRepository<Perfil>, IPerfilRepository
{
    public PerfilRepository(NayraBaseDbContext context) : base(context)
    {
    }

    public async Task<Perfil?> GetByNombreAsync(string nombre)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.Nombre == nombre);
    }

    public async Task<Perfil?> GetByIdWithPermisosAsync(int id)
    {
        return await _dbSet
            .Include(p => p.PerfilPermisos)
                .ThenInclude(pp => pp.Permiso)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Perfil>> GetActivosAsync()
    {
        return await _dbSet
            .Where(p => p.Activo)
            .OrderBy(p => p.NivelJerarquico)
            .ToListAsync();
    }
}