//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace NayraBase.Infrastructure.Data.Configurations
//{
//    internal class PermisoConfiguration
//    {
//    }
//}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NayraBase.Domain.Entities;

namespace NayraBase.Infrastructure.Data.Configurations;

/// <summary>
/// Configuración de Entity Framework Core para la entidad Permiso
/// </summary>
public class PermisoConfiguration : IEntityTypeConfiguration<Permiso>
{
    public void Configure(EntityTypeBuilder<Permiso> builder)
    {
        // Nombre de tabla
        builder.ToTable("permisos");

        // Llave primaria
        builder.HasKey(p => p.Id);

        // Propiedades
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(p => p.Codigo)
            .HasColumnName("codigo")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Modulo)
            .HasColumnName("modulo")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Recurso)
            .HasColumnName("recurso")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Accion)
            .HasColumnName("accion")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.Alcance)
            .HasColumnName("alcance")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.EsOperativo)
            .HasColumnName("es_operativo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.EsSupervision)
            .HasColumnName("es_supervision")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.EsAuditoria)
            .HasColumnName("es_auditoria")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.Descripcion)
            .HasColumnName("descripcion")
            .HasColumnType("text");

        builder.Property(p => p.RequiereJustificacion)
            .HasColumnName("requiere_justificacion")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.NivelSensibilidad)
            .HasColumnName("nivel_sensibilidad")
            .IsRequired()
            .HasDefaultValue(1);

        builder.Property(p => p.Activo)
            .HasColumnName("activo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(p => p.EsSistema)
            .HasColumnName("es_sistema")
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(p => p.FechaCreacion)
            .HasColumnName("fecha_creacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(p => p.FechaActualizacion)
            .HasColumnName("fecha_actualizacion")
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Índices
        builder.HasIndex(p => p.Codigo)
            .IsUnique()
            .HasDatabaseName("idx_permiso_codigo");

        builder.HasIndex(p => p.Modulo)
            .HasDatabaseName("idx_permiso_modulo");

        builder.HasIndex(p => p.Recurso)
            .HasDatabaseName("idx_permiso_recurso");

        // Relaciones
        builder.HasMany(p => p.PerfilPermisos)
            .WithOne(pp => pp.Permiso)
            .HasForeignKey(pp => pp.PermisoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}