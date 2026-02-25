//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class DependenciaPerfilConfigConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad DependenciaPerfilConfig
/// </summary>
public class DependenciaPerfilConfigConfiguration : IEntityTypeConfiguration<DependenciaPerfilConfig>
{
    public void Configure(EntityTypeBuilder<DependenciaPerfilConfig> builder)
    {
        // Nombre de tabla
        builder.ToTable("dependencia_perfil_config");

        // Llave primaria
        builder.HasKey(dpc => dpc.Id);

        // Propiedades
        builder.Property(dpc => dpc.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(dpc => dpc.DependenciaId)
            .HasColumnName("dependencia_id")
            .IsRequired();

        builder.Property(dpc => dpc.PerfilId)
            .HasColumnName("perfil_id")
            .IsRequired();

        builder.Property(dpc => dpc.Activo)
            .HasColumnName("activo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(dpc => dpc.CantidadMaxima)
            .HasColumnName("cantidad_maxima");

        builder.Property(dpc => dpc.RequiereAprobacion)
            .HasColumnName("requiere_aprobacion")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(dpc => dpc.ConfiguradoPor)
            .HasColumnName("configurado_por");

        builder.Property(dpc => dpc.FechaConfiguracion)
            .HasColumnName("fecha_configuracion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(dpc => dpc.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(dpc => dpc.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índice único compuesto
        builder.HasIndex(dpc => new { dpc.DependenciaId, dpc.PerfilId })
            .IsUnique()
            .HasDatabaseName("idx_dependencia_perfil_unique");

        // Relaciones
        builder.HasOne(dpc => dpc.Dependencia)
            .WithMany(d => d.ConfiguracionPerfiles)
            .HasForeignKey(dpc => dpc.DependenciaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dpc => dpc.Perfil)
            .WithMany(p => p.ConfiguracionesDependencias)
            .HasForeignKey(dpc => dpc.PerfilId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}