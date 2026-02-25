//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Context
//{
//    internal class NayraBaseDbContext
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Context;

/// <summary>
/// Contexto principal de Entity Framework Core para NayraBase
/// </summary>
public class NayraBaseDbContext : DbContext
{
    public NayraBaseDbContext(DbContextOptions<NayraBaseDbContext> options)
        : base(options)
    {
    }

    // DbSets - Representan las tablas en la base de datos

    public DbSet<Persona> Personas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<TokenConfirmacion> TokensConfirmacion { get; set; }
    public DbSet<Dependencia> Dependencias { get; set; }
    public DbSet<Perfil> Perfiles { get; set; }
    public DbSet<Permiso> Permisos { get; set; }
    public DbSet<ModoHerencia> ModosHerencia { get; set; }
    public DbSet<PerfilPermiso> PerfilPermisos { get; set; }
    public DbSet<DependenciaPerfilConfig> DependenciaPerfilConfigs { get; set; }
    public DbSet<UsuarioPerfilDependencia> UsuarioPerfilDependencias { get; set; }
    public DbSet<ClienteApi> ClientesApi { get; set; }
    public DbSet<ClienteApiLog> ClienteApiLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar todas las configuraciones del assembly actual
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NayraBaseDbContext).Assembly);
    }
}