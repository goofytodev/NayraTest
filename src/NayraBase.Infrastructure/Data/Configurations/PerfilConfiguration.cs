//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class PerfilConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad Perfil
/// </summary>
public class PerfilConfiguration : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        // Nombre de tabla
        builder.ToTable("perfiles");

        // Llave primaria
        builder.HasKey(p => p.Id);

        // Propiedades
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Descripcion)
            .HasColumnName("descripcion")
            .HasColumnType("text");

        builder.Property(p => p.NivelJerarquico)
            .HasColumnName("nivel_jerarquico")
            .IsRequired()
            .HasDefaultValue(5);

        builder.Property(p => p.EsOperativo)
            .HasColumnName("es_operativo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.EsSupervision)
            .HasColumnName("es_supervision")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.Activo)
            .HasColumnName("activo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(p => p.Nombre)
            .IsUnique()
            .HasDatabaseName("idx_perfil_nombre");

        // Relaciones
        builder.HasMany(p => p.PerfilPermisos)
            .WithOne(pp => pp.Perfil)
            .HasForeignKey(pp => pp.PerfilId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.ConfiguracionesDependencias)
            .WithOne(dpc => dpc.Perfil)
            .HasForeignKey(dpc => dpc.PerfilId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(p => p.AsignacionesUsuarios)
            .WithOne(upd => upd.Perfil)
            .HasForeignKey(upd => upd.PerfilId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}