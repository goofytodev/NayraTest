using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Repositories.Interfaces;

public interface IModoHerenciaRepository : IGenericRepository<ModoHerencia>
{
    Task<ModoHerencia?> GetByCodigoAsync(string codigo);
    Task<IEnumerable<ModoHerencia>> GetActivosAsync();
    Task<IEnumerable<ModoHerencia>> GetSistemaAsync();
}