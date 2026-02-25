using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Repositories.Interfaces;

public interface IPermisoRepository : IGenericRepository<Permiso>
{
    Task<Permiso?> GetByCodigoAsync(string codigo);
    Task<IEnumerable<Permiso>> GetByModuloAsync(string modulo);
    Task<IEnumerable<Permiso>> GetPermisosOperativosAsync();
    Task<IEnumerable<Permiso>> GetPermisosSupervisionAsync();
}