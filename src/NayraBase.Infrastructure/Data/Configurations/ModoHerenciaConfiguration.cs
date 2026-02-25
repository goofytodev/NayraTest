//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class ModoHerenciaConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad ModoHerencia
/// </summary>
public class ModoHerenciaConfiguration : IEntityTypeConfiguration<ModoHerencia>
{
    public void Configure(EntityTypeBuilder<ModoHerencia> builder)
    {
        // Nombre de tabla
        builder.ToTable("modos_herencia");

        // Llave primaria
        builder.HasKey(mh => mh.Id);

        // Propiedades
        builder.Property(mh => mh.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(mh => mh.Codigo)
            .HasColumnName("codigo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(mh => mh.Nombre)
            .HasColumnName("nombre")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(mh => mh.Descripcion)
            .HasColumnName("descripcion")
            .HasColumnType("text");

        builder.Property(mh => mh.PermiteOperaciones)
            .HasColumnName("permite_operaciones")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(mh => mh.PermiteLectura)
            .HasColumnName("permite_lectura")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(mh => mh.PermiteReportes)
            .HasColumnName("permite_reportes")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(mh => mh.PermiteAprobaciones)
            .HasColumnName("permite_aprobaciones")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(mh => mh.FiltroPermisos)
            .HasColumnName("filtro_permisos")
            .HasColumnType("jsonb"); // PostgreSQL JSONB

        builder.Property(mh => mh.ColorHex)
            .HasColumnName("color_hex")
            .HasMaxLength(7);

        builder.Property(mh => mh.Icono)
            .HasColumnName("icono")
            .HasMaxLength(50);

        builder.Property(mh => mh.NivelAcceso)
            .HasColumnName("nivel_acceso")
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(mh => mh.EsSistema)
            .HasColumnName("es_sistema")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(mh => mh.Activo)
            .HasColumnName("activo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(mh => mh.CreadoPor)
            .HasColumnName("creado_por");

        builder.Property(mh => mh.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(mh => mh.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(mh => mh.Codigo)
            .IsUnique()
            .HasDatabaseName("idx_modo_herencia_codigo");

        // Relaciones
        builder.HasMany(mh => mh.AsignacionesConModo)
            .WithOne(upd => upd.ModoHerencia)
            .HasForeignKey(upd => upd.ModoHerenciaId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}