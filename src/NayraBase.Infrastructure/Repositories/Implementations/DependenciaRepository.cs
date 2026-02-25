using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Repositories.Interfaces;

namespace NayraBase.Infrastructure.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de Dependencia
/// </summary>
public class DependenciaRepository : GenericRepository<Dependencia>, IDependenciaRepository
{
    public DependenciaRepository(NayraBaseDbContext context) : base(context)
    {
    }

    public async Task<Dependencia?> GetByCodigoAsync(string codigo)
    {
        return await _dbSet
            .FirstOrDefaultAsync(d => d.Codigo == codigo);
    }

    public async Task<IEnumerable<Dependencia>> GetHijasAsync(int dependenciaPadreId)
    {
        return await _dbSet
            .Where(d => d.DependenciaPadreId == dependenciaPadreId)
            .OrderBy(d => d.Orden)
            .ToListAsync();
    }

    public async Task<IEnumerable<Dependencia>> GetRaicesAsync()
    {
        return await _dbSet
            .Where(d => d.DependenciaPadreId == null)
            .OrderBy(d => d.Orden)
            .ToListAsync();
    }

    public async Task<IEnumerable<Dependencia>> GetArbolCompletoAsync()
    {
        return await _dbSet
            .Include(d => d.DependenciaPadre)
            .Include(d => d.DependenciasHijas)
            .OrderBy(d => d.Nivel)
            .ThenBy(d => d.Orden)
            .ToListAsync();
    }

    public async Task<IEnumerable<Dependencia>> GetAncestrosAsync(int dependenciaId)
    {
        var ancestros = new List<Dependencia>();
        var dependencia = await _dbSet
            .Include(d => d.DependenciaPadre)
            .FirstOrDefaultAsync(d => d.Id == dependenciaId);

        while (dependencia?.DependenciaPadre != null)
        {
            ancestros.Add(dependencia.DependenciaPadre);
            dependencia = await _dbSet
                .Include(d => d.DependenciaPadre)
                .FirstOrDefaultAsync(d => d.Id == dependencia.DependenciaPadreId);
        }

        return ancestros;
    }

    public async Task<IEnumerable<Dependencia>> GetDescendientesAsync(int dependenciaId)
    {
        // Query recursivo para obtener todos los descendientes
        var sql = @"
            WITH RECURSIVE descendientes AS (
                SELECT id, nombre, codigo, dependencia_padre_id, nivel
                FROM dependencias
                WHERE id = {0}
                
                UNION ALL
                
                SELECT d.id, d.nombre, d.codigo, d.dependencia_padre_id, d.nivel
                FROM dependencias d
                INNER JOIN descendientes desc ON d.dependencia_padre_id = desc.id
            )
            SELECT id FROM descendientes WHERE id != {0}
        ";

        var descendientesIds = await _context.Database
            .SqlQueryRaw<int>(sql, dependenciaId)
            .ToListAsync();

        return await _dbSet
            .Where(d => descendientesIds.Contains(d.Id))
            .OrderBy(d => d.Nivel)
            .ThenBy(d => d.Orden)
            .ToListAsync();
    }
}