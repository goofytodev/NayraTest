using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Repositories.Interfaces;

public interface IPerfilRepository : IGenericRepository<Perfil>
{
    Task<Perfil?> GetByNombreAsync(string nombre);
    Task<Perfil?> GetByIdWithPermisosAsync(int id);
    Task<IEnumerable<Perfil>> GetActivosAsync();
}