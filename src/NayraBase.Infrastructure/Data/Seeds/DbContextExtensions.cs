using NayraBase.Infrastructure.Data.Context;

namespace NayraBase.Infrastructure.Data.Seeds;

/// <summary>
/// Métodos de extensión para el DbContext
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    /// Siembra los datos iniciales en la base de datos
    /// </summary>
    public static async Task SeedDatabaseAsync(this NayraBaseDbContext context)
    {
        await NayraBaseDataSeeder.SeedAsync(context);
    }
}
