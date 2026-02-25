using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Repositories.Interfaces;

public interface IDependenciaRepository : IGenericRepository<Dependencia>
{
    Task<Dependencia?> GetByCodigoAsync(string codigo);
    Task<IEnumerable<Dependencia>> GetHijasAsync(int dependenciaPadreId);
    Task<IEnumerable<Dependencia>> GetRaicesAsync();
    Task<IEnumerable<Dependencia>> GetArbolCompletoAsync();
    Task<IEnumerable<Dependencia>> GetAncestrosAsync(int dependenciaId);
    Task<IEnumerable<Dependencia>> GetDescendientesAsync(int dependenciaId);
}