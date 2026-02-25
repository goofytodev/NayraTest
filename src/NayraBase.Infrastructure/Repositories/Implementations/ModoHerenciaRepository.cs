using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Repositories.Interfaces;

namespace NayraBase.Infrastructure.Repositories.Implementations;

public class ModoHerenciaRepository : GenericRepository<ModoHerencia>, IModoHerenciaRepository
{
    public ModoHerenciaRepository(NayraBaseDbContext context) : base(context)
    {
    }

    public async Task<ModoHerencia?> GetByCodigoAsync(string codigo)
    {
        return await _dbSet.FirstOrDefaultAsync(m => m.Codigo == codigo);
    }

    public async Task<IEnumerable<ModoHerencia>> GetActivosAsync()
    {
        return await _dbSet
            .Where(m => m.Activo)
            .OrderBy(m => m.NivelAcceso)
            .ToListAsync();
    }

    public async Task<IEnumerable<ModoHerencia>> GetSistemaAsync()
    {
        return await _dbSet
            .Where(m => m.EsSistema)
            .OrderBy(m => m.NivelAcceso)
            .ToListAsync();
    }
}