using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Repositorio específico para Persona
/// </summary>
public interface IPersonaRepository : IGenericRepository<Persona>
{
    Task<Persona?> GetByNumeroDocumentoAsync(string numeroDocumento);
    Task<Persona?> GetByEmailAsync(string email);
    Task<bool> ExistsNumeroDocumentoAsync(string numeroDocumento, int? excludeId = null);
    Task<IEnumerable<Persona>> SearchAsync(string searchTerm);
    Task<Persona?> GetByIdWithUsuarioAsync(int id);
}