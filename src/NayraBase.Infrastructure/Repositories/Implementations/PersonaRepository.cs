using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;
using NayraBase.Infrastructure.Data.Context;
using NayraBase.Infrastructure.Repositories.Interfaces;

namespace NayraBase.Infrastructure.Repositories.Implementations;

/// <summary>
/// Implementación del repositorio de Persona
/// </summary>
public class PersonaRepository : GenericRepository<Persona>, IPersonaRepository
{
    public PersonaRepository(NayraBaseDbContext context) : base(context)
    {
    }

    public async Task<Persona?> GetByNumeroDocumentoAsync(string numeroDocumento)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.NumeroDocumento == numeroDocumento);
    }

    public async Task<Persona?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.Email == email);
    }

    public async Task<bool> ExistsNumeroDocumentoAsync(string numeroDocumento, int? excludeId = null)
    {
        var query = _dbSet.Where(p => p.NumeroDocumento == numeroDocumento);

        if (excludeId.HasValue)
        {
            query = query.Where(p => p.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }

    public async Task<IEnumerable<Persona>> SearchAsync(string searchTerm)
    {
        var term = searchTerm.ToLower();

        return await _dbSet
            .Where(p =>
                p.Nombres.ToLower().Contains(term) ||
                p.ApellidoPaterno.ToLower().Contains(term) ||
                (p.ApellidoMaterno != null && p.ApellidoMaterno.ToLower().Contains(term)) ||
                p.NumeroDocumento.Contains(term) ||
                (p.Email != null && p.Email.ToLower().Contains(term))
            )
            .ToListAsync();
    }

    public async Task<Persona?> GetByIdWithUsuarioAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}