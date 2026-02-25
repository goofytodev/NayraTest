//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class DependenciaConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad Dependencia
/// Implementa estructura de árbol jerárquico con auto-referencia
/// </summary>
public class DependenciaConfiguration : IEntityTypeConfiguration<Dependencia>
{
    public void Configure(EntityTypeBuilder<Dependencia> builder)
    {
        // Nombre de tabla
        builder.ToTable("dependencias");

        // Llave primaria
        builder.HasKey(d => d.Id);

        // Propiedades
        builder.Property(d => d.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(d => d.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(d => d.Codigo)
            .HasColumnName("codigo")
            .HasMaxLength(20);

        builder.Property(d => d.Descripcion)
            .HasColumnName("descripcion")
            .HasColumnType("text");

        builder.Property(d => d.DependenciaPadreId)
            .HasColumnName("dependencia_padre_id");

        builder.Property(d => d.Nivel)
            .HasColumnName("nivel")
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(d => d.RutaCompleta)
            .HasColumnName("ruta_completa")
            .HasColumnType("text");

        builder.Property(d => d.EsHoja)
            .HasColumnName("es_hoja")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(d => d.Orden)
            .HasColumnName("orden")
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(d => d.Estado)
            .HasColumnName("estado")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(d => d.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(d => d.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(d => d.Codigo)
            .IsUnique()
            .HasDatabaseName("idx_dependencia_codigo");

        builder.HasIndex(d => d.DependenciaPadreId)
            .HasDatabaseName("idx_dependencia_padre");

        builder.HasIndex(d => d.Nivel)
            .HasDatabaseName("idx_dependencia_nivel");

        // Relaciones - Auto-referencia (árbol jerárquico)
        builder.HasOne(d => d.DependenciaPadre)
            .WithMany(d => d.DependenciasHijas)
            .HasForeignKey(d => d.DependenciaPadreId)
            .OnDelete(DeleteBehavior.Restrict); // No permitir eliminar si tiene hijos

        builder.HasMany(d => d.DependenciasHijas)
            .WithOne(d => d.DependenciaPadre)
            .HasForeignKey(d => d.DependenciaPadreId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(d => d.ConfiguracionPerfiles)
            .WithOne(dpc => dpc.Dependencia)
            .HasForeignKey(dpc => dpc.DependenciaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(d => d.UsuariosAsignados)
            .WithOne(upd => upd.Dependencia)
            .HasForeignKey(upd => upd.DependenciaId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}