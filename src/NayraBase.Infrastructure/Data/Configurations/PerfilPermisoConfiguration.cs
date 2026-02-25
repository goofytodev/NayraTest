//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class PerfilPermisoConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad PerfilPermiso
/// Relación muchos a muchos entre Perfil y Permiso
/// </summary>
public class PerfilPermisoConfiguration : IEntityTypeConfiguration<PerfilPermiso>
{
    public void Configure(EntityTypeBuilder<PerfilPermiso> builder)
    {
        // Nombre de tabla
        builder.ToTable("perfil_permiso");

        // Llave primaria
        builder.HasKey(pp => pp.Id);

        // Propiedades
        builder.Property(pp => pp.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(pp => pp.PerfilId)
            .HasColumnName("perfil_id")
            .IsRequired();

        builder.Property(pp => pp.PermisoId)
            .HasColumnName("permiso_id")
            .IsRequired();

        builder.Property(pp => pp.PuedeDelegar)
            .HasColumnName("puede_delegar")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(pp => pp.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(pp => pp.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índice único compuesto (no repetir misma combinación)
        builder.HasIndex(pp => new { pp.PerfilId, pp.PermisoId })
            .IsUnique()
            .HasDatabaseName("idx_perfil_permiso_unique");

        // Relaciones
        builder.HasOne(pp => pp.Perfil)
            .WithMany(p => p.PerfilPermisos)
            .HasForeignKey(pp => pp.PerfilId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pp => pp.Permiso)
            .WithMany(p => p.PerfilPermisos)
            .HasForeignKey(pp => pp.PermisoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}